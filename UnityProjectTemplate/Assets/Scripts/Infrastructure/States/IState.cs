using Cysharp.Threading.Tasks;

namespace Game.Infrastructure.States
{
	public interface IState : IExitableState
	{
		UniTask Enter();
	}
}