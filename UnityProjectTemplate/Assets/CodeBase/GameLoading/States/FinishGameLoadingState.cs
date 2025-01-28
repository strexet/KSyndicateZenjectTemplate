﻿using CodeBase.Infrastructure.States;
using CodeBase.Services.LogService;
using Cysharp.Threading.Tasks;

namespace CodeBase.GameLoading.States
{
	public class FinishGameLoadingState : IState
	{
		readonly GameStateMachine gameStateMachine;
		readonly ILogService log;

		public FinishGameLoadingState(GameStateMachine gameStateMachine, ILogService log)
		{
			this.gameStateMachine = gameStateMachine;
			this.log = log;
		}

		public async UniTask Enter()
		{
			log.Log("FinishGameLoadingState enter");

			gameStateMachine.Enter<GameHubState>().Forget();
		}

		public UniTask Exit() =>
			default;
	}
}