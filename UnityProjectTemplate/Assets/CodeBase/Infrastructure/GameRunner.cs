using UnityEngine;
using Zenject;

namespace CodeBase.Infrastructure
{
	public class GameRunner : MonoBehaviour
	{
		GameBootstrapper.Factory gameBootstrapperFactory;

		[Inject]
		void Construct(GameBootstrapper.Factory bootstrapperFactory) => gameBootstrapperFactory = bootstrapperFactory;

		void Awake()
		{
			var bootstrapper = FindObjectOfType<GameBootstrapper>();

			if (bootstrapper != null)
			{
				return;
			}

			gameBootstrapperFactory.Create();
		}

	}
}