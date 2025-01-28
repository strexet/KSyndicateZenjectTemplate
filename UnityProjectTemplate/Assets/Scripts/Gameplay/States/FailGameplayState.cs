using Cysharp.Threading.Tasks;
using Game.Infrastructure.States;

namespace Game.Gameplay.States
{
	/// <summary>
	///     Use such states for showing fail screens and offering resurrections and so on.
	/// </summary>
	public class FailGameplayState : IState
	{
		public UniTask Enter() => default;

		public UniTask Exit() => default;
	}
}