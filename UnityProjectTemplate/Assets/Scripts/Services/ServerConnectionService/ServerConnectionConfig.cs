using UnityEngine;

namespace CodeBase.Services.ServerConnectionService
{
	// You can add any necessary information for your specific game into this config.
	[CreateAssetMenu(menuName = "Configs/Services/ConnectionService")]
	public class ServerConnectionConfig : ScriptableObject
	{
		// Following settings are here for demonstration purpose only.
		public string ServerAddress;
		public int Port;
		public float ConnectionTimeout;
	}
}