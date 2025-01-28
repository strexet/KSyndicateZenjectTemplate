using Cysharp.Threading.Tasks;

namespace Game.Infrastructure.States
{
	public interface IExitableState
	{
		UniTask Exit();
	}
}