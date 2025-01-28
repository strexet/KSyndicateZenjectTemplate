using Cysharp.Threading.Tasks;
using Game.UI.PopUps.PolicyAcceptPopup;

namespace Game.UI.Services.PopUps
{
	public interface IPopUpService
	{
		UniTask<bool> AskPolicyPopup(PolicyAcceptPopupConfig config);

		UniTask ShowError(string messageHeader, string messageBody, string buttonText = "OK");
	}
}