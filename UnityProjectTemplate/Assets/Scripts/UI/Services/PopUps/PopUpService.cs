﻿using Cysharp.Threading.Tasks;
using Game.UI.PopUps.ErrorPopup;
using Game.UI.PopUps.PolicyAcceptPopup;
using Game.UI.Services.Factories;
using System;
using System.Threading;
using Object = UnityEngine.Object;

namespace Game.UI.Services.PopUps
{
	public class PopUpService : IPopUpService, IDisposable
	{
		readonly IUIFactory uiFactory;
		readonly ErrorPopupConfig errorPopupConfig;
		readonly CancellationTokenSource ctn;

		public PopUpService(IUIFactory uiFactory)
		{
			this.uiFactory = uiFactory;
			errorPopupConfig = new ErrorPopupConfig();
			ctn = new CancellationTokenSource();
		}

		public void Dispose() => ctn.Cancel();

		public async UniTask<bool> AskPolicyPopup(PolicyAcceptPopupConfig config)
		{
			var popup = await uiFactory.CreatePolicyAskingPopup();
			var result = await popup.Show(config).AttachExternalCancellation(ctn.Token);
			Object.Destroy(popup);

			return result;
		}

		public async UniTask ShowError(string messageHeader, string messageBody, string buttonText = "OK")
		{
			errorPopupConfig.HeaderText = messageHeader;
			errorPopupConfig.MessageText = messageBody;
			errorPopupConfig.ButtonText = buttonText;

			var errorPopup = await uiFactory.CreateErrorPopup();
			await errorPopup.Show(errorPopupConfig).AttachExternalCancellation(ctn.Token);

			errorPopup.Hide();
		}
	}
}