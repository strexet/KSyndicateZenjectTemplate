using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.UI.LoadingCurtain;
using CodeBase.Services.AdsService;
using CodeBase.Services.AnalyticsService;
using CodeBase.Services.LogService;
using CodeBase.Services.StaticDataService;
using CodeBase.UI.Overlays;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
	public class GameBootstrapState : IState
	{
		readonly GameStateMachine gameStateMachine;
		readonly IAdsService adsService;
		readonly IStaticDataService staticDataService;
		readonly IAnalyticsService analyticsService;
		readonly ILogService log;
		readonly LoadingCurtainProxy loadingCurtainProxy;
		readonly AwaitingOverlayProxy awaitingOverlayProxy;
		readonly IAssetProvider assetProvider;

		public GameBootstrapState(GameStateMachine gameStateMachine,
			IAdsService adsService,
			IStaticDataService staticDataService,
			IAnalyticsService analyticsService,
			IAssetProvider assetProvider,
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