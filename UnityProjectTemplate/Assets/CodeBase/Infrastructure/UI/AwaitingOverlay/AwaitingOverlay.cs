﻿using CodeBase.Services.LocalizationService;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Overlays
{
	public class AwaitingOverlay : MonoBehaviour, IAwaitingOverlay
	{
		[SerializeField]
		TextMeshProUGUI message;
		[SerializeField]
		Canvas canvas;

		ILocalizationService localizationService;

		void Awake() =>
			Hide();

		public void Show(string withMessage)
		{
			message.text = localizationService.Translate(withMessage);
			canvas.enabled = true;
		}

		public void Hide() => canvas.enabled = false;

		[Inject]
		public void Construct(ILocalizationService localizationService) =>
			this.localizationService = localizationService;

		public class Factory : PlaceholderFactory<string, UniTask<AwaitingOverlay>> { }
	}
}