using Cysharp.Threading.Tasks;
using Game.Infrastructure.States;

namespace Game.Gameplay.States
{
	/// <summary>
	///     Use such states for finishing gameplay and cleanup resources, posting session statistics and leaving Game State.
	/// </summary>
	public class FinishGameplayState : IState
	{
		readonly GameStateMachine gameStateMachine;

		public FinishGameplayState(GameStateMachine gameStateMachine) => this.gameStateMachine = gameStateMachine;

		public async UniTask Exit() => gameStateMachine.Enter<GameHubState>().Forget();

		public UniTask Enter() => default;
	}
}