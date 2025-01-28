using Cysharp.Threading.Tasks;
using Game.Infrastructure.AssetManagement;
using Game.Infrastructure.UI.AwaitingOverlay;
using Game.Infrastructure.UI.LoadingCurtain;
using Game.Services.AdsService;
using Game.Services.AnalyticsService;
using Game.Services.LogService;
using Game.Services.StaticDataService;

namespace Game.Infrastructure.States
{
	public class GameBootstrapState : IState
	{
		readonly GameStateMachine gameStateMachine;
		readonly AdsService adsService;
		readonly StaticDataService staticDataService;
		readonly AnalyticsService analyticsService;
		readonly ILogService log;
		readonly LoadingCurtainProxy loadingCurtainProxy;
		readonly AwaitingOverlayProxy awaitingOverlayProxy;
		readonly AssetProvider assetProvider;

		public GameBootstrapState(GameStateMachine gameStateMachine,
			AdsService adsService,
			StaticDataService staticDataService,
			AnalyticsService analyticsService,
			AssetProvider assetProvider,
			ILogService log,
			LoadingCurtainProxy loadingCurtainProxy,
			AwaitingOverlayProxy awaitingOverlayProxy)
		{
			this.adsService = adsService;
			this.staticDataService = staticDataService;
			this.gameStateMachine = gameStateMachine;
			this.staticDataService = staticDataService;
			this.analyticsService = analyticsService;
			this.assetProvider = assetProvider;
			this.log = log;
			this.loadingCurtainProxy = loadingCurtainProxy;
			this.awaitingOverlayProxy = awaitingOverlayProxy;
		}

		public async UniTask Enter()
		{
			log.Log("BootstrapState Enter");

			await InitServices();

			gameStateMachine.Enter<GameLoadingState>().Forget();
		}

		public UniTask Exit() => default;

		async UniTask InitServices()
		{
			// Init global services that may need initialization in some order here
			await assetProvider.InitializeAsync();
			await staticDataService.InitializeAsync();
			await loadingCurtainProxy.InitializeAsync();
			await awaitingOverlayProxy.InitializeAsync();

			analyticsService.Initialize();
			adsService.Initialize();
		}
	}
}