using Cysharp.Threading.Tasks;
using Game.Gameplay.States;
using Game.Infrastructure.States;
using Game.Services.LogService;
using Zenject;

namespace Game.Gameplay
{
	public class GameplaySceneBootstraper : IInitializable
	{
		readonly SceneStateMachine sceneStateMachine;
		readonly StatesFactory statesFactory;
		readonly ILogService log;

		public GameplaySceneBootstraper(SceneStateMachine sceneStateMachine, StatesFactory statesFactory, ILogService log)
		{
			this.sceneStateMachine = sceneStateMachine;
			this.statesFactory = statesFactory;
			this.log = log;
		}

		public void Initialize()
		{
			log.Log("Start game mode scene bootstraping");

			sceneStateMachine.RegisterState(statesFactory.Create<StartGameplayState>());
			sceneStateMachine.RegisterState(statesFactory.Create<PlayGameplayState>());
			sceneStateMachine.RegisterState(statesFactory.Create<WinGameplayState>());
			sceneStateMachine.RegisterState(statesFactory.Create<FailGameplayState>());
			sceneStateMachine.RegisterState(statesFactory.Create<FinishGameplayState>());

			log.Log("Finish game mode scene bootstraping");

			// Go to the first state for this scene
			sceneStateMachine.Enter<StartGameplayState>().Forget();
		}
	}
}