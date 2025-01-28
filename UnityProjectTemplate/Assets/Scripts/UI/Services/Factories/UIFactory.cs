using Cysharp.Threading.Tasks;
using Game.UI.PopUps.ErrorPopup;
using Game.UI.PopUps.PolicyAcceptPopup;

namespace Game.UI.Services.Factories
{
	public class UIFactory : IUIFactory
	{
		readonly PolicyAcceptPopup.Factory privatePolicyWindowFactory;
		readonly ErrorPopup.Factory errorPopupFactory;

		public UIFactory(PolicyAcceptPopup.Factory privatePolicyWindowFactory, ErrorPopup.Factory errorPopupFactory)
		{
			this.privatePolicyWindowFactory = privatePolicyWindowFactory;
			this.errorPopupFactory = errorPopupFactory;
		}

		public UniTask<PolicyAcceptPopup> CreatePolicyAskingPopup() => privatePolicyWindowFactory.Create(UIFactoryAssets.PolicyAcceptPopup);

		public UniTask<ErrorPopup> CreateErrorPopup() => errorPopupFactory.Create(UIFactoryAssets.ErrorPopup);

		public void Cleanup() { }
	}
}