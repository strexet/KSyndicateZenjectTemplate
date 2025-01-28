using CodeBase.Infrastructure.States;
using CodeBase.Services.LogService;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace CodeBase.UI.Elements
{
	public class GameStateSwitchButton : MonoBehaviour
	{
		public enum TargetStates
		{
			None = 0,
			Loading = 1,
			GameHub = 2,
			Gameplay = 3,
		}

		[SerializeField] TargetStates targetState = 0;
		[SerializeField] Button button;

		GameStateMachine gameStateMachine;
		ILogService log;

		void OnEnable() => button.onClick.AddListener(OnClick);

		void OnDisable() => button.onClick.RemoveListener(OnClick);

		[Inject]
		void Construct(GameStateMachine gameStateMachine, ILogService log)
		{
			this.gameStateMachine = gameStateMachine;
			this.log = log;
		}

		void OnClick()
		{
			switch (targetState)
			{
				case TargetStates.Loading:
					gameStateMachine.Enter<GameLoadingState>().Forget();
					break;

				case TargetStates.GameHub:
					gameStateMachine.Enter<GameHubState>().Forget();
					break;

				case TargetStates.Gameplay:
					gameStateMachine.Enter<GameplayState>().Forget();
					break;

				default:
					log.LogError("Not valid option");
					break;
			}
		}
	}
}