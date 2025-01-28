using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace CodeBase.Services.PrefabPoolingService
{
	public sealed class PrefabPoolingService : IPrefabPoolingService
	{
		readonly SpecificPrefabPool.Factory poolsFactory;

		readonly Dictionary<int, SpecificPrefabPool> prefabsToPoolsMap;
		readonly Dictionary<int, SpecificPrefabPool> objectToPoolMap;
		IInstantiator instantiator;

		public PrefabPoolingService(IInstantiator instantiator, SpecificPrefabPool.Factory poolsFactory)
		{
			this.instantiator = instantiator;
			this.poolsFactory = poolsFactory;
			prefabsToPoolsMap = new Dictionary<int, SpecificPrefabPool>();
			objectToPoolMap = new Dictionary<int, SpecificPrefabPool>();
		}

		public GameObject Spawn(GameObject prefab, Transform parent = null)
		{
			SpecificPrefabPool pool = null;
			var prefabInstanceId = prefab.GetInstanceID();

			if (!prefabsToPoolsMap.TryGetValue(prefabInstanceId, out pool))
			{
				pool = CreateNewPool(prefab);
			}

			var spawnedObject = pool.Spawn(parent);
			objectToPoolMap.Add(spawnedObject.GetInstanceID(), pool);

			return spawnedObject;
		}

		public TComponent Spawn<TComponent>(TComponent prefab, Transform parent = null) where TComponent : MonoBehaviour
		{
			var spawnedObject = Spawn(prefab.gameObject, parent);
			return spawnedObject.GetComponent<TComponent>();
		}

		public void Despawn(GameObject gameObject)
		{
			var instanceID = gameObject.GetInstanceID();

			if (objectToPoolMap.TryGetValue(instanceID, out var pool))
			{
				objectToPoolMap.Remove(instanceID);
				pool.Despawn(gameObject);
			}
		}

		public void Despawn<TComponent>(TComponent gameObject) where TComponent : MonoBehaviour => Despawn(gameObject.gameObject);

		SpecificPrefabPool CreateNewPool(GameObject prefab)
		{
			SpecificPrefabPool pool;
			pool = poolsFactory.Create(prefab);
			pool.Initialize();
			prefabsToPoolsMap.Add(prefab.GetInstanceID(), pool);
			return pool;
		}
	}
}