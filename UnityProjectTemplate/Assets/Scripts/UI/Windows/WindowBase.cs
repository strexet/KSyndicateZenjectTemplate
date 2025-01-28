using Game.Data;
using Game.Services.PlayerProgressService;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI.Windows
{
	public abstract class WindowBase : MonoBehaviour
	{
		[SerializeField] protected Button CloseButton;

		protected PersistentProgressService ProgressService;

		protected PlayerProgress Progress => ProgressService.Progress;

		[Inject]
		public void Construct(PersistentProgressService progressService) =>
			ProgressService = progressService;

		void Awake() => OnAwake();

		void Start()
		{
			Initialize();
			SubscribeUpdates();
		}

		void OnDestroy() => Cleanup();

		protected virtual void OnAwake() => CloseButton?.onClick.AddListener(() => Destroy(gameObject));

		protected virtual void Initialize() { }

		protected virtual void SubscribeUpdates() { }

		protected virtual void Cleanup() { }
	}
}