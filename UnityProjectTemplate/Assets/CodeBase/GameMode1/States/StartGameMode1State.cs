using CodeBase.Infrastructure.States;
using Cysharp.Threading.Tasks;

namespace CodeBase.GameMode1.States
{
    public class StartGameMode1State : IState
    {
        private readonly SceneStateMachine sceneStateMachine;

        public StartGameMode1State(SceneStateMachine sceneStateMachine) => 
            this.sceneStateMachine = sceneStateMachine;

        public async UniTask Enter()
        {
            // you can use states like this for showing starting cut scenes, objectives on the level, explaining game rules and so on
            sceneStateMachine.Enter<PlayGameMode1State>().Forget();
        }

        public UniTask Exit()
        {
            return default;
        }
    }
}