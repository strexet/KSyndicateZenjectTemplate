using Game.UI.Services.Factories;
using System;

namespace Game.UI.Services.Window
{
	public class WindowService
	{
		readonly IUIFactory uiFactory;

		public WindowService(IUIFactory uiFactory) => this.uiFactory = uiFactory;

		public void Open(WindowId window)
		{
			switch (window)
			{
				case WindowId.None:
					break;

				case WindowId.PrivatePolicyAccept:
					uiFactory.CreatePolicyAskingPopup();
					break;

				default:
					throw new ArgumentOutOfRangeException(nameof(window), window, null);
			}
		}
	}
}