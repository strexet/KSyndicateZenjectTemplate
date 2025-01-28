using CodeBase.Services.LocalizationService;
using CodeBase.Services.LogService;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.PopUps.PolicyAcceptPopup
{
	public class PolicyAcceptPopup : PopUpBase<bool, PolicyAcceptPopupConfig>
	{
		[SerializeField] Toggle toggle;
		[SerializeField] Button button;
		[SerializeField] TextMeshProUGUI policyText;
		[SerializeField] TextMeshProUGUI agreeText;
		[SerializeField] TextMeshProUGUI buttonText;

		ILocalizationService localizationService;
		ILogService log;

		[Inject]
		public void Construct(ILocalizationService localizationService, ILogService log)
		{
			this.localizationService = localizationService;
			this.log = log;
		}

		override protected void Initialize(PolicyAcceptPopupConfig config)
		{
			base.Initialize(config);
			FillData(config);
			SetControlStates();
		}

		void SetControlStates()
		{
			toggle.isOn = false;
			UpdateCloseButton(false);
		}

		void FillData(PolicyAcceptPopupConfig config)
		{
			policyText.text = localizationService.Translate(config.PolicyText);
			agreeText.text = localizationService.Translate(config.AgreeText);
			buttonText.text = localizationService.Translate(config.ButtonText);
		}

		override protected void SubscribeUpdates()
		{
			base.SubscribeUpdates();
			toggle.onValueChanged.AddListener(OnToggleChange);
			button.onClick.AddListener(OnButtonClick);
		}

		void OnToggleChange(bool value)
		{
			log.Log($"Private policy acceptance set to: {value}");
			UpdateCloseButton(value);
		}

		void UpdateCloseButton(bool enable) => button.interactable = enable;

		override protected void Cleanup()
		{
			base.Cleanup();
			toggle.onValueChanged.RemoveListener(OnToggleChange);
			button.onClick.RemoveListener(OnButtonClick);
		}

		void OnButtonClick() => SetPopUpResult(toggle.isOn);

		public class Factory : PlaceholderFactory<string, UniTask<PolicyAcceptPopup>> { }
	}
}