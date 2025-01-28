using Cysharp.Threading.Tasks;
using Game.Infrastructure.States;
using Game.Infrastructure.UI.AwaitingOverlay;
using Game.Services.LogService;
using Game.Services.ServerConnectionService;
using Game.Services.StaticDataService;
using Game.UI.Services.PopUps;

namespace Game.GameLoading.States
{
	public class ServerConnectState : IState
	{
		readonly IServerConnectionService serverConnectionService;
		readonly IStaticDataService staticDataService;
		readonly SceneStateMachine sceneStateMachine;
		readonly IAwaitingOverlay awaitingOverlay;
		readonly IPopUpService popUpService;
		readonly ILogService log;

		public ServerConnectState(IServerConnectionService serverConnectionService,
			IStaticDataService staticDataService,
			SceneStateMachine sceneStateMachine,
			IAwaitingOverlay awaitingOverlay,
			IPopUpService popUpService,
			ILogService log)
		{
			this.serverConnectionService = serverConnectionService;
			this.staticDataService = staticDataService;
			this.sceneStateMachine = sceneStateMachine;
			this.awaitingOverlay = awaitingOverlay;
			this.popUpService = popUpService;
			this.log = log;
		}

		public async UniTask Enter()
		{
			log.Log("ServerConnectState enter");

			awaitingOverlay.Show("Connection to server...");

			var result = await serverConnectionService.Connect(staticDataService.ServerConnectionConfig);
			awaitingOverlay.Hide();

			if (result == ConnectionResult.Success)
			{
				sceneStateMachine.Enter<LoadPlayerProgressState>().Forget();
			}
			else
			{
				// Do some connection error handling here
				// (for example, show error popup or try to connect again)
				await popUpService.ShowError("Connection error",
											 "Can't connect to server. Please check your internet connection.");
			}
		}

		public UniTask Exit() => default;
	}
}