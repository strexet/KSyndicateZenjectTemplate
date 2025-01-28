using Cysharp.Threading.Tasks;
using Game.Infrastructure;
using Game.Infrastructure.AssetManagement;
using Game.Infrastructure.Factories;
using Game.Infrastructure.SceneManagement;
using Game.Infrastructure.States;
using Game.Infrastructure.UI.AwaitingOverlay;
using Game.Infrastructure.UI.LoadingCurtain;
using Game.Services.AdsService;
using Game.Services.AnalyticsService;
using Game.Services.InputService;
using Game.Services.LocalizationService;
using Game.Services.LogService;
using Game.Services.PlayerProgressService;
using Game.Services.RandomizerService;
using Game.Services.SaveLoadService;
using Game.Services.ServerConnectionService;
using Game.Services.StaticDataService;
using Game.Services.WalletService;
using Zenject;

namespace Game.CompositionRoot
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindLogService();
			BindInputService();

			BindGameBootstraperFactory();
			BindCoroutineRunner();
			BindRandomizeService();
			BindGameStateMachine();
			BindGameFactory();
			BindStaticDataService();
			BindAssetProvider();

			BindSceneLoader();
			BindServerConnectionService();
			BindPlayerProgressService();
			BindSaveLoadService();

			BindInfrastructureUI();
			BindWalletService();
			BindLocalizationService();

			BindAnalyticsService();
			BindAdsService();
		}

		void BindLogService() => Container.BindInterfacesTo<LogService>()
										  .AsSingle();

		void BindInputService() => Container.BindInterfacesAndSelfTo<InputService>()
											.AsSingle();

		void BindGameBootstraperFactory() => Container.BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
													  .FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstraper);

		void BindCoroutineRunner() => Container.Bind<ICoroutineRunner>()
											   .To<CoroutineRunner>()
											   .FromComponentInNewPrefabResource(InfrastructureAssetPath.CoroutineRunnerPath)
											   .AsSingle();

		void BindRandomizeService() => Container.Bind<RandomizerService>()
												.AsSingle();

		void BindGameStateMachine() => GameStateMachineInstaller.Install(Container);

		void BindGameFactory() => Container.Bind<GameFactory>()
										   .FromSubContainerResolve()
										   .ByInstaller<GameFactoryInstaller>()
										   .AsSingle();

		void BindStaticDataService() => Container.Bind<StaticDataService>()
												 .AsSingle();

		void BindAssetProvider() => Container.Bind<AssetProvider>()
											 .AsSingle();

		void BindSceneLoader() => Container.Bind<SceneLoader>()
										   .AsSingle();

		void BindServerConnectionService() => Container.Bind<ServerConnectionService>()
													   .AsSingle();

		void BindPlayerProgressService() => Container.Bind<PersistentProgressService>()
													 .AsSingle();

		void BindSaveLoadService() => Container.Bind<SaveLoadService>()
											   .AsSingle();

		void BindInfrastructureUI()
		{
			BindLoadingCurtains();
			BindAwaitingOverlay();
		}

		void BindLoadingCurtains()
		{
			Container.BindFactory<string, UniTask<LoadingCurtain>, LoadingCurtain.Factory>()
					 .FromFactory<PrefabFactoryAsync<LoadingCurtain>>();

			Container.BindInterfacesAndSelfTo<LoadingCurtainProxy>()
					 .AsSingle();
		}

		void BindAwaitingOverlay()
		{
			Container.BindFactory<string, UniTask<AwaitingOverlay>, AwaitingOverlay.Factory>()
					 .FromFactory<PrefabFactoryAsync<AwaitingOverlay>>();

			Container.BindInterfacesAndSelfTo<AwaitingOverlayProxy>()
					 .AsSingle();
		}

		void BindWalletService() => Container.Bind<WalletService>()
											 .AsSingle();

		void BindLocalizationService() => Container.Bind<LocalizationService>()
												   .AsSingle();

		void BindAnalyticsService() => Container.Bind<AnalyticsService>()
												.AsSingle();

		void BindAdsService() => Container.Bind<AdsService>()
										  .AsSingle();
	}
}