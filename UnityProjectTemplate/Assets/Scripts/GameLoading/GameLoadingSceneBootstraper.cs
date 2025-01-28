using Cysharp.Threading.Tasks;
using Game.GameLoading.States;
using Game.Infrastructure.States;
using Game.Services.LogService;
using Zenject;

namespace Game.GameLoading
{
	public class GameLoadingSceneBootstraper : IInitializable
	{
		readonly SceneStateMachine sceneStateMachine;
		readonly StatesFactory statesFactory;
		readonly ILogService log;

		public GameLoadingSceneBootstraper(SceneStateMachine sceneStateMachine, StatesFactory statesFactory, ILogService log)
		{
			this.sceneStateMachine = sceneStateMachine;
			this.statesFactory = statesFactory;
			this.log = log;
		}

		public void Initialize()
		{
			log.Log("Start loading scene bootstraping");

			sceneStateMachine.RegisterState(statesFactory.Create<ServerConnectState>());
			sceneStateMachine.RegisterState(statesFactory.Create<LoadPlayerProgressState>());
			sceneStateMachine.RegisterState(statesFactory.Create<PrivatePolicyState>());
			sceneStateMachine.RegisterState(statesFactory.Create<GDPRState>());
			sceneStateMachine.RegisterState(statesFactory.Create<FinishGameLoadingState>());

			log.Log("Finish loading scene bootstraping");

			// Go to the first state for this scene
			sceneStateMachine.Enter<ServerConnectState>().Forget();
		}
	}
}