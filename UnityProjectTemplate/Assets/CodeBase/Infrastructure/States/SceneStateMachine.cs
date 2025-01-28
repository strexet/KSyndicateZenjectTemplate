namespace CodeBase.Infrastructure.States
{
	/// <summary>
	///     Local state machine for switching scene states (for example, states of a gameplay level).
	///     It is bound in a scene context and filled with different scene states according to a scene logic.
	///     Scene bootstrapers fills this FSM with scene dependant states.
	/// </summary>
	public class SceneStateMachine : StateMachine { }
}