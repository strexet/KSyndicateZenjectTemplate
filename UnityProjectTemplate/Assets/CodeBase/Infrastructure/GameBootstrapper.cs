﻿using CodeBase.Infrastructure.States;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
	public class GameBootstrapper : MonoBehaviour
	{
		GameStateMachine gameStateMachine;
		StatesFactory statesFactory;

		[Inject]
		void Construct(GameStateMachine gameStateMachine, StatesFactory statesFactory)
		{
			this.gameStateMachine = gameStateMachine;
			this.statesFactory = statesFactory;
		}

		void Awake()
		{
			gameStateMachine.RegisterState(statesFactory.Create<GameBootstrapState>());
			gameStateMachine.RegisterState(statesFactory.Create<GameLoadingState>());
			gameStateMachine.RegisterState(statesFactory.Create<GameHubState>());
			gameStateMachine.RegisterState(statesFactory.Create<GameplayState>());

			gameStateMachine.Enter<GameBootstrapState>().Forget();

			DontDestroyOnLoad(this);
		}

		public class Factory : PlaceholderFactory<GameBootstrapper> { }
	}
}