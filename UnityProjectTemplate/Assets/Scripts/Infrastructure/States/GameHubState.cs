using Cysharp.Threading.Tasks;
using Game.Infrastructure.AssetManagement;
using Game.Infrastructure.SceneManagement;
using Game.Infrastructure.UI.LoadingCurtain;
using Game.Services.LogService;

namespace Game.Infrastructure.States
{
	public class GameHubState : IState
	{
		readonly ILoadingCurtain loadingCurtain;
		readonly ISceneLoader sceneLoader;
		readonly ILogService log;
		readonly IAssetProvider assetProvider;

		public GameHubState(ILoadingCurtain loadingCurtain,
			ISceneLoader sceneLoader,
			ILogService log,
			IAssetProvider assetProvider)
		{
			this.loadingCurtain = loadingCurtain;
			this.sceneLoader = sceneLoader;
			this.log = log;
			this.assetProvider = assetProvider;
		}

		public async UniTask Enter()
		{
			log.Log("GameHub state enter");

			loadingCurtain.Show();

			await assetProvider.WarmupAssetsByLabel(AssetLabels.GameHubState);

			// Because we don't have any substates for this state now, we just load scene with game hub decorations.
			await sceneLoader.Load(InfrastructureAssetPath.GameHubScene);

			loadingCurtain.Hide();
		}

		public async UniTask Exit()
		{
			loadingCurtain.Show();
			await assetProvider.ReleaseAssetsByLabel(AssetLabels.GameHubState);
		}
	}
}