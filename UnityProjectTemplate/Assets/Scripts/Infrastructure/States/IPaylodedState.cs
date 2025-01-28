using Cysharp.Threading.Tasks;

namespace Game.Infrastructure.States
{
	public interface IPaylodedState<TPayload> : IExitableState
	{
		UniTask Enter(TPayload payload);
	}
}