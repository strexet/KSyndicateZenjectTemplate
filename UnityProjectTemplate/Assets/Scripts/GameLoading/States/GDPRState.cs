using Cysharp.Threading.Tasks;
using Game.Infrastructure.States;
using Game.Services.LogService;
using Game.Services.PlayerProgressService;
using Game.Services.StaticDataService;
using Game.UI.PopUps.PolicyAcceptPopup;
using Game.UI.Services.PopUps;
using System.Threading.Tasks;

namespace Game.GameLoading.States
{
	public class GDPRState : IState
	{
		readonly IPopUpService popUpService;
		readonly SceneStateMachine sceneStateMachine;
		readonly IPersistentProgressService progressService;
		readonly ILogService log;
		readonly IStaticDataService staticDataService;

		public GDPRState(IPopUpService popUpService,
			IStaticDataService staticDataService,
			SceneStateMachine sceneStateMachine,
			IPersistentProgressService progressService,
			ILogService log)
		{
			this.popUpService = popUpService;
			this.sceneStateMachine = sceneStateMachine;
			this.progressService = progressService;
			this.log = log;
			this.staticDataService = staticDataService;
		}

		public async UniTask Enter()
		{
			log.Log("GDPRState enter");

			if (!progressService.Progress.GDPRPolicyAccepted)
			{
				await AskToAcceptGDPRPolicy();
			}

			if (progressService.Progress.GDPRPolicyAccepted)
			{
				sceneStateMachine.Enter<FinishGameLoadingState>().Forget();
			}
			else
			{
				log.Log("Player cant play our game due to reject gdpr policy :)");
			}
		}

		public UniTask Exit() => default;

		async Task AskToAcceptGDPRPolicy()
		{
			var popupConfig = staticDataService.GetPolicyAcceptPopupConfig(PolicyAcceptPopupTypes.GDPR);
			var result = await popUpService.AskPolicyPopup(popupConfig);
			progressService.Progress.GDPRPolicyAccepted = result;
		}
	}
}