using Cysharp.Threading.Tasks;
using Game.Infrastructure.States;

namespace Game.Gameplay.States
{
	/// <summary>
	///     You can use states like this for showing starting cut scenes, objectives on the level, explaining game rules and so on.
	/// </summary>
	public class StartGameplayState : IState
	{
		readonly SceneStateMachine sceneStateMachine;

		public StartGameplayState(SceneStateMachine sceneStateMachine) => this.sceneStateMachine = sceneStateMachine;

		public async UniTask Enter() => sceneStateMachine.Enter<PlayGameplayState>().Forget();

		public UniTask Exit() => default;
	}
}