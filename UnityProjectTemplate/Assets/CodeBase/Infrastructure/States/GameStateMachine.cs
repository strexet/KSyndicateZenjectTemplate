namespace CodeBase.Infrastructure.States
{
	/// <summary>
	///     Global state machine that switches global states of the game.
	///     It is bound in the project context to be available globally.
	///     Game bootstraper fills this FSM with global states.
	/// </summary>
	public class GameStateMachine : StateMachine { }
}