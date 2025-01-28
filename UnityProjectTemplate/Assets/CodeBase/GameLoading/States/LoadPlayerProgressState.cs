﻿using CodeBase.Data;
using CodeBase.Infrastructure.States;
using CodeBase.Services.LogService;
using CodeBase.Services.PlayerProgressService;
using CodeBase.Services.SaveLoadService;
using CodeBase.Services.WalletService;
using CodeBase.UI.Overlays;
using Cysharp.Threading.Tasks;
using System.Collections.Generic;

namespace CodeBase.GameLoading.States
{
	public class LoadPlayerProgressState : IState
	{
		readonly SceneStateMachine sceneStateMachine;
		readonly ISaveLoadService saveLoadService;
		readonly IEnumerable<IProgressReader> progressReaderServices;
		readonly IPersistentProgressService progressService;
		readonly IAwaitingOverlay awaitingOverlay;
		readonly ILogService log;

		public LoadPlayerProgressState(SceneStateMachine sceneStateMachine,
			IPersistentProgressService progressService,
			ISaveLoadService saveLoadService,
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