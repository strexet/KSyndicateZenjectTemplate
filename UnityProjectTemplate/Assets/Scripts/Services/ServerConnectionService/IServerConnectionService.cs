using Cysharp.Threading.Tasks;

namespace Game.Services.ServerConnectionService
{
	public interface IServerConnectionService
	{
		UniTask<ConnectionResult> Connect(ServerConnectionConfig config);
	}
}