using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure.AssetManagement
{
	public class PrefabFactoryAsync<TComponent> : IFactory<string, UniTask<TComponent>>
	{
		readonly IInstantiator instantiator;
		readonly IAssetProvider assetProvider;

		public PrefabFactoryAsync(IInstantiator instantiator,
			IAssetProvider assetProvider)
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