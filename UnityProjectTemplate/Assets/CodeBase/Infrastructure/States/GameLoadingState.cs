using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.UI.LoadingCurtain;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
	public class GameLoadingState : IState
	{
		readonly ILoadingCurtain loadingCurtain;
		readonly ISceneLoader sceneLoader;
		readonly IAssetProvider assetProvider;

		public GameLoadingState(ILoadingCurtain loadingCurtain,
			ISceneLoader sceneLoader,
			IAssetProvider assetProvider)
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