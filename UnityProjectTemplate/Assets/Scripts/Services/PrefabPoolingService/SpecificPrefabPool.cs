﻿using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Services.PrefabPoolingService
{
	public class SpecificPrefabPool
	{
		readonly IInstantiator instantiator;
		readonly GameObject prefab;

		readonly Queue<GameObject> pool;
		GameObject poolRoot;

		public SpecificPrefabPool(GameObject prefab, IInstantiator instantiator)
		{
			this.prefab = prefab;
			this.instantiator = instantiator;
			pool = new Queue<GameObject>();
		}

		public void Initialize()
		{
			poolRoot = new GameObject($"{prefab.name}_pool_{Guid.NewGuid()}");
			GameObject.DontDestroyOnLoad(poolRoot);
		}

		public GameObject Spawn(Transform parent)
		{
			GameObject result = null;

			if (!pool.TryDequeue(out result))
			{
				result = instantiator.InstantiatePrefab(prefab);
			}

			result.transform.SetParent(parent, false);
			result.SetActive(true);

			return result;
		}

		public void Despawn(GameObject gameObject)
		{
			gameObject.SetActive(false);
			gameObject.transform.SetParent(poolRoot.transform);
			pool.Enqueue(gameObject);
		}

		public class Factory : PlaceholderFactory<GameObject, SpecificPrefabPool> { }
	}
}