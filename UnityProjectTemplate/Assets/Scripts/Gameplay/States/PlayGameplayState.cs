using Cysharp.Threading.Tasks;
using Game.Infrastructure.States;

namespace Game.Gameplay.States
{
	/// <summary>
	///     Use such states for actual gameplay.
	/// </summary>
	public class PlayGameplayState : IState
	{
		public UniTask Enter() => default;

		public UniTask Exit() => default;
	}
}