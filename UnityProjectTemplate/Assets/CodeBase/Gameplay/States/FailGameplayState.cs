using CodeBase.Infrastructure.States;
using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.States
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