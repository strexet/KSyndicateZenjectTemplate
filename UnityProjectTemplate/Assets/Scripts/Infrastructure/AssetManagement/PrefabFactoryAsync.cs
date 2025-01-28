using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.AssetManagement
{
	public class PrefabFactoryAsync<TComponent> : IFactory<string, UniTask<TComponent>>
	{
		readonly IInstantiator instantiator;
		readonly AssetProvider assetProvider;

		public PrefabFactoryAsync(IInstantiator instantiator,
			AssetProvider assetProvider)
		{
			this.instantiator = instantiator;
			this.assetProvider = assetProvider;
		}

		public async UniTask<TComponent> Create(string assetKey)
		{
			var prefab = await assetProvider.Load<GameObject>(assetKey);
			var newObject = instantiator.InstantiatePrefab(prefab);
			return newObject.GetComponent<TComponent>();
		}
	}
}