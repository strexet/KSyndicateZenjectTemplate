using CodeBase.Infrastructure.States;
using Cysharp.Threading.Tasks;

namespace CodeBase.Gameplay.States
{
	/// <summary>
	/// Use such states for showing congratulation screens and offering bonuses for ads.
	/// </summary>
	public class WinGameplayState : IState
	{
		public UniTask Exit() =>
			default;

		public UniTask Enter() => default;
	}
}