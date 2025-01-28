using CodeBase.Infrastructure.States;
using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.States
{
	/// <summary>
	/// Use such states for actual gameplay.
	/// </summary>
	public class PlayGameplayState : IState
	{
		public UniTask Enter() =>
			
			default;

		public UniTask Exit() => default;
	}
}