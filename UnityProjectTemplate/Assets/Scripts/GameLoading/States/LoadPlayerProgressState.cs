using Cysharp.Threading.Tasks;
using Game.Data;
using Game.Infrastructure.States;
using Game.Infrastructure.UI.AwaitingOverlay;
using Game.Services.LogService;
using Game.Services.PlayerProgressService;
using Game.Services.SaveLoadService;
using Game.Services.WalletService;
using System.Collections.Generic;

namespace Game.GameLoading.States
{
	public class LoadPlayerProgressState : IState
	{
		readonly SceneStateMachine sceneStateMachine;
		readonly SaveLoadService saveLoadService;
		readonly IEnumerable<IProgressReader> progressReaderServices;
		readonly PersistentProgressService progressService;
		readonly IAwaitingOverlay awaitingOverlay;
		readonly ILogService log;

		public LoadPlayerProgressState(SceneStateMachine sceneStateMachine,
			PersistentProgressService progressService,
			SaveLoadService saveLoadService,
			IEnumerable<IProgressReader> progressReaderServices,
			IAwaitingOverlay awaitingOverlay,
			ILogService log)
		{
			this.sceneStateMachine = sceneStateMachine;
			this.saveLoadService = saveLoadService;
			this.progressService = progressService;
			this.progressReaderServices = progressReaderServices;
			this.awaitingOverlay = awaitingOverlay;
			this.log = log;
		}

		public async UniTask Enter()
		{
			log.Log("LoadPlayerProgressState enter");

			awaitingOverlay.Show("Loading player progress...");

			var progress = LoadProgressOrInitNew();
			NotifyProgressReaderServices(progress);

			await UniTask.WaitForSeconds(1f); // just for demonstrate concept with overlay. You can remove it. 
			awaitingOverlay.Hide();

			sceneStateMachine.Enter<PrivatePolicyState>().Forget();
		}

		public UniTask Exit()
		{
			log.Log("LoadPlayerProgressState exit");
			return default;
		}

		void NotifyProgressReaderServices(PlayerProgress progress)
		{
			foreach (var reader in progressReaderServices)
			{
				reader.LoadProgress(progress);
			}
		}

		PlayerProgress LoadProgressOrInitNew()
		{
			progressService.Progress = saveLoadService.LoadProgress() ?? NewProgress();
			return progressService.Progress;
		}

		PlayerProgress NewProgress()
		{
			log.Log("Init new player progress");

			// Create new progress state
			var progress = new PlayerProgress {
				PrivatePolicyAccepted = false,
				GDPRPolicyAccepted = false,
				WalletsData = new WalletsData(new Dictionary<int, long>()),
			};

			return progress;
		}
	}
}