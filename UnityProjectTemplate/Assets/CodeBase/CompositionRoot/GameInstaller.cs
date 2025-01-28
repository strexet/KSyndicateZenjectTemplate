using CodeBase.Infrastructure;
using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.Factories;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.States;
using CodeBase.Infrastructure.UI.LoadingCurtain;
using CodeBase.Services.AdsService;
using CodeBase.Services.AnalyticsService;
using CodeBase.Services.InputService;
using CodeBase.Services.LocalizationService;
using CodeBase.Services.LogService;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.RandomizerService;
using CodeBase.Services.SaveLoadService;
using CodeBase.Services.ServerConnectionService;
using CodeBase.Services.StaticDataService;
using CodeBase.Services.WalletService;
using CodeBase.UI.Overlays;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.CompositionRoot
{
	public class GameInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			BindGameBootstraperFactory();
			BindCoroutineRunner();
			BindRandomizeService();
			BindGameStateMachine();
			BindGameFactory();
			BindStaticDataService();
			BindAssetProvider();
			BindInputService();

			BindSceneLoader();
			BindServerConnectionService();
			BindPlayerProgressService();
			BindSaveLoadService();

			BindInfrastructureUI();
			BindWalletService();
			BindLocalizationService();

			BindAnalyticsService();
			BindAdsService();
			BindLogService();
		}

		void BindGameBootstraperFactory() => Container
											.BindFactory<GameBootstrapper, GameBootstrapper.Factory>()
											.FromComponentInNewPrefabResource(InfrastructureAssetPath.GameBootstraper);

		void BindCoroutineRunner() => Container
									 .Bind<ICoroutineRunner>()
									 .To<CoroutineRunner>()
									 .FromComponentInNewPrefabResource(InfrastructureAssetPath.CoroutineRunnerPath)
									 .AsSingle();

		void BindRandomizeService() => Container
									  .BindInterfacesAndSelfTo<RandomizerService>()
									  .AsSingle();

		void BindGameStateMachine() => GameStateMachineInstaller.Install(Container);

		void BindGameFactory() => Container
								 .Bind<IGameFactory>()
								 .FromSubContainerResolve()
								 .ByInstaller<GameFactoryInstaller>()
								 .AsSingle();

		void BindStaticDataService() => Container
									   .BindInterfacesAndSelfTo<StaticDataService>()
									   .AsSingle();

		void BindAssetProvider() => Container
								   .BindInterfacesTo<AssetProvider>()
								   .AsSingle();

		void BindInputService() => Container
								  .BindInterfacesAndSelfTo<InputService>()
								  .AsSingle();

		void BindSceneLoader() => Container
								 .BindInterfacesAndSelfTo<SceneLoader>()
								 .AsSingle();

		void BindServerConnectionService() => Container
											 .BindInterfacesTo<ServerConnectionService>()
											 .AsSingle();

		void BindPlayerProgressService() => Container
										   .BindInterfacesAndSelfTo<PersistentProgressService>()
										   .AsSingle();

		void BindSaveLoadService() => Container
									 .BindInterfacesAndSelfTo<SaveLoadService>()
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

		void BindWalletService() => Container
								   .BindInterfacesAndSelfTo<WalletService>()
								   .AsSingle();

		void BindLocalizationService() => Container
										 .BindInterfacesTo<LocalizationService>()
										 .AsSingle();

		void BindAnalyticsService() => Container
									  .BindInterfacesTo<AnalyticsService>()
									  .AsSingle();

		void BindAdsService() => Container
								.BindInterfacesAndSelfTo<AdsService>()
								.AsSingle();

		void BindLogService() => Container
								.BindInterfacesTo<LogService>()
								.AsSingle();
	}
}