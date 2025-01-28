using CodeBase.Data;
using CodeBase.Services.PlayerProgressService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.PopUps
{
	public abstract class PopUpBase<TResult, TInitializeData> : MonoBehaviour
	{
		[SerializeField] Canvas popupCanvas;

		protected IPersistentProgressService ProgressService;

		UniTaskCompletionSource<TResult> taskCompletionSource;

		protected PlayerProgress Progress => ProgressService.Progress;

		[Inject]
		public void Construct(IPersistentProgressService progressService) => ProgressService = progressService;

		void Awake() => OnAwake();

		void OnDestroy() => Cleanup();

		public UniTask<TResult> Show(TInitializeData with)
		{
			taskCompletionSource = new UniTaskCompletionSource<TResult>();
			Initialize(with);
			SubscribeUpdates();
			popupCanvas.enabled = true;
			return taskCompletionSource.Task;
		}

		public void Hide() => popupCanvas.enabled = false;

		protected void SetPopUpResult(TResult result) => taskCompletionSource.TrySetResult(result);

		protected virtual void OnAwake() => Hide();

		protected virtual void Initialize(TInitializeData with) { }

		protected virtual void SubscribeUpdates() { }

		protected virtual void Cleanup() { }
	}
}