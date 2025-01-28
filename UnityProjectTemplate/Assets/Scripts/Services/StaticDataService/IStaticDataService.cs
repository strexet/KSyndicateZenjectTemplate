using Cysharp.Threading.Tasks;
using Game.Services.ServerConnectionService;
using Game.UI.PopUps.PolicyAcceptPopup;

namespace Game.Services.StaticDataService
{
	public interface IStaticDataService
	{
		ServerConnectionConfig ServerConnectionConfig { get; }

		UniTask InitializeAsync();

		PolicyAcceptPopupConfig GetPolicyAcceptPopupConfig(PolicyAcceptPopupTypes type);
	}
}