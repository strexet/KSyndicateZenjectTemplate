using Cysharp.Threading.Tasks;

namespace CodeBase.Services.ServerConnectionService
{
	// If we have some server or BaaS that store our progress or give multiplayer possibilities we can implement
	// connection in this class

	public class ServerConnectionService : IServerConnectionService
	{
		public async UniTask<ConnectionResult> Connect(ServerConnectionConfig config)
		{
			// Imitate connection process
			await UniTask.WaitForSeconds(1f).AsTask();

			return ConnectionResult.Success;
		}
	}
}