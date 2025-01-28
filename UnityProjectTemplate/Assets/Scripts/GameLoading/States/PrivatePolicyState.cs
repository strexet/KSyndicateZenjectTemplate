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
	public class PrivatePolicyState : IState
	{
		readonly IPopUpService popUpService;
		readonly SceneStateMachine sceneStateMachine;
		readonly PersistentProgressService progressService;
		readonly ILogService log;
		readonly StaticDataService staticData;

		public PrivatePolicyState(IPopUpService popUpService,
			StaticDataService staticData,
			SceneStateMachine sceneStateMachine,
			PersistentProgressService progressService,
			ILogService log)
		{
			this.popUpService = popUpService;
			this.sceneStateMachine = sceneStateMachine;
			this.progressService = progressService;
			this.log = log;
			this.staticData = staticData;
		}

		public async UniTask Enter()
		{
			log.Log("PrivatePolicyState enter");

			if (!progressService.Progress.PrivatePolicyAccepted)
			{
				await AskToAcceptPrivatePolicy();
			}

			if (progressService.Progress.PrivatePolicyAccepted)
			{
				sceneStateMachine.Enter<GDPRState>().Forget();
			}
			else
			{
				log.Log("Player cant play our game due to somehow reject private policy :)");
			}
		}

		public UniTask Exit() => default;

		async Task AskToAcceptPrivatePolicy()
		{
			var popupConfig = staticData.GetPolicyAcceptPopupConfig(PolicyAcceptPopupTypes.PrivatePolicy);
			var result = await popUpService.AskPolicyPopup(popupConfig);
			progressService.Progress.PrivatePolicyAccepted = result;
		}
	}
}