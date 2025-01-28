using Cysharp.Threading.Tasks;
using Game.Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure
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