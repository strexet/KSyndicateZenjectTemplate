using Cysharp.Threading.Tasks;
using Game.UI.PopUps.ErrorPopup;
using Game.UI.PopUps.PolicyAcceptPopup;

namespace Game.UI.Services.Factories
{
	public interface IUIFactory
	{
		void Cleanup();

		UniTask<PolicyAcceptPopup> CreatePolicyAskingPopup();

		UniTask<ErrorPopup> CreateErrorPopup();
	}
}