﻿using CodeBase.Infrastructure.AssetManagement;
using CodeBase.Infrastructure.SceneManagement;
using CodeBase.Infrastructure.UI.LoadingCurtain;
using CodeBase.Services.LogService;
using Cysharp.Threading.Tasks;

namespace CodeBase.Infrastructure.States
{
	public class GameplayState : IState
	{
		readonly ILoadingCurtain loadingCurtain;
		readonly ISceneLoader sceneLoader;
		readonly ILogService log;
		readonly IAssetProvider assetProvider;

		public GameplayState(ILoadingCurtain loadingCurtain,
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
			log.Log("Game mode 1 state enter");
			loadingCurtain.Show();

			await assetProvider.WarmupAssetsByLabel(AssetLabels.GameplayState);
			await sceneLoader.Load(InfrastructureAssetPath.GameMode1Scene);

			loadingCurtain.Hide();
		}

		public async UniTask Exit()
		{
			loadingCurtain.Show();
			await assetProvider.ReleaseAssetsByLabel(AssetLabels.GameplayState);
		}
	}
}