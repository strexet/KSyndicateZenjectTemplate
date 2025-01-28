using Cysharp.Threading.Tasks;
using Game.Infrastructure.AssetManagement;
using Game.Infrastructure.SceneManagement;
using Game.Infrastructure.UI.LoadingCurtain;

namespace Game.Infrastructure.States
{
	public class GameLoadingState : IState
	{
		readonly ILoadingCurtain loadingCurtain;
		readonly SceneLoader sceneLoader;
		readonly AssetProvider assetProvider;

		public GameLoadingState(ILoadingCurtain loadingCurtain,
			SceneLoader sceneLoader,
			AssetProvider assetProvider)
		{
			this.loadingCurtain = loadingCurtain;
			this.sceneLoader = sceneLoader;
			this.assetProvider = assetProvider;
		}

		public async UniTask Enter()
		{
			loadingCurtain.Show();

			await assetProvider.WarmupAssetsByLabel(AssetLabels.GameLoadingState);
			await sceneLoader.Load(InfrastructureAssetPath.GameLoadingScene);

			loadingCurtain.Hide();
		}

		public async UniTask Exit()
		{
			loadingCurtain.Show();

			await assetProvider.ReleaseAssetsByLabel(AssetLabels.GameLoadingState);
		}
	}
}