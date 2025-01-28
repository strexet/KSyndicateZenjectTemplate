using Game.Infrastructure.States;
using Game.Services.PrefabPoolingService;
using Game.UI;
using UnityEngine;
using Zenject;

namespace Game.Gameplay
{
	public class GameplaySceneInstaller : MonoInstaller
	{
		// Here we bind dependencies that make sense only in a scene that is loading.
		// If we need some dependencies after the scene finished loading,
		// we can bind them here and use them afterward from the scene context.
		public override void InstallBindings()
		{
			Debug.Log("Start game scene installer");

			// NonLazy due to the fact that it is not injected anywhere, but we still need to instantiate it.
			Container.BindInterfacesAndSelfTo<GameplaySceneBootstraper>()
					 .AsSingle()
					 .NonLazy();

			Container.BindInterfacesAndSelfTo<StatesFactory>()
					 .AsSingle();

			Container.Bind<SceneStateMachine>()
					 .AsSingle();

			UIInstaller.Install(Container);

			Container.BindInterfacesAndSelfTo<PrefabPoolingService>()
					 .AsSingle();
		}
	}
}