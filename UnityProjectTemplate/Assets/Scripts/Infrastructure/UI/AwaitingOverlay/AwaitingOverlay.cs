using Cysharp.Threading.Tasks;
using Game.Services.LocalizationService;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.UI.AwaitingOverlay
{
	public class AwaitingOverlay : MonoBehaviour, IAwaitingOverlay
	{
		[SerializeField] TextMeshProUGUI message;
		[SerializeField] Canvas canvas;

		LocalizationService localizationService;

		[Inject]
		public void Construct(LocalizationService localizationService) => this.localizationService = localizationService;

		void Awake() => Hide();

		public void Show(string withMessage)
		{
			message.text = localizationService.Translate(withMessage);
			canvas.enabled = true;
		}

		public void Hide() => canvas.enabled = false;

		public class Factory : PlaceholderFactory<string, UniTask<AwaitingOverlay>> { }
	}
}