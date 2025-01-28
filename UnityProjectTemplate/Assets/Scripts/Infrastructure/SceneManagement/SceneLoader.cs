using Cysharp.Threading.Tasks;
using Game.Services.LogService;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

namespace Game.Infrastructure.SceneManagement
{
	public class SceneLoader : ISceneLoader
	{
		readonly ILogService log;

		public SceneLoader(ILogService log) => this.log = log;

		public async UniTask Load(string nextScene)
		{
			log.Log($"Loading scene {nextScene}");

			var handler = Addressables.LoadSceneAsync(nextScene, LoadSceneMode.Single, false);

			await handler.ToUniTask();
			await handler.Result.ActivateAsync().ToUniTask();
		}
	}
}