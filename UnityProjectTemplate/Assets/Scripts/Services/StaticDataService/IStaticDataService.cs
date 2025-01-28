using CodeBase.Services.ServerConnectionService;
using CodeBase.UI.PopUps.PolicyAcceptPopup;
using Cysharp.Threading.Tasks;

namespace CodeBase.Services.StaticDataService
{
	public interface IStaticDataService
	{
		ServerConnectionConfig ServerConnectionConfig { get; }

		UniTask InitializeAsync();

		PolicyAcceptPopupConfig GetPolicyAcceptPopupConfig(PolicyAcceptPopupTypes type);
	}
}