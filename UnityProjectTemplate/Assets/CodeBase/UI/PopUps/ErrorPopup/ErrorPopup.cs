using CodeBase.Services.LocalizationService;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.PopUps.ErrorPopup
{
	public class ErrorPopup : PopUpBase<bool, ErrorPopupConfig>
	{
		[SerializeField] TextMeshProUGUI headerText;
		[SerializeField] TextMeshProUGUI messageText;
		[SerializeField] TextMeshProUGUI buttonText;
		[SerializeField] Button button;

		ILocalizationService localizationService;

		public void Construct(ILocalizationService localizationService) => this.localizationService = localizationService;

		override protected void Initialize(ErrorPopupConfig with)
		{
			base.Initialize(with);
			headerText.text = localizationService.Translate(with.HeaderText);
			messageText.text = localizationService.Translate(with.MessageText);
			buttonText.text = localizationService.Translate(with.ButtonText);
		}

		override protected void SubscribeUpdates()
		{
			base.SubscribeUpdates();
			button.onClick.AddListener(OnClick);
		}

		void OnClick() => SetPopUpResult(true);

		override protected void Cleanup()
		{
			base.Cleanup();
			button.onClick.RemoveListener(OnClick);
		}

		public class Factory : PlaceholderFactory<string, UniTask<ErrorPopup>> { }
	}
}