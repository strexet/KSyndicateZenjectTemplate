using Cysharp.Threading.Tasks;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Infrastructure.UI.LoadingCurtain
{
	public class LoadingCurtain : MonoBehaviour, ILoadingCurtain
	{
		public CanvasGroup Curtain;

		public void Show()
		{
			gameObject.SetActive(true);
			Curtain.alpha = 1;
		}

		public void Hide() => StartCoroutine(DoFadeIn());

		IEnumerator DoFadeIn()
		{
			while (Curtain.alpha > 0)
			{
				Curtain.alpha -= 0.03f;
				yield return new WaitForSeconds(0.03f);
			}

			gameObject.SetActive(false);
		}

		public class Factory : PlaceholderFactory<string, UniTask<LoadingCurtain>> { }
	}
}