﻿using Cysharp.Threading.Tasks;
using Game.Infrastructure.AssetManagement;
using Game.Infrastructure.SceneManagement;
using Game.Infrastructure.UI.LoadingCurtain;
using Game.Services.LogService;

namespace Game.Infrastructure.States
{
	public class GameplayState : IState
	{
		readonly ILoadingCurtain loadingCurtain;
		readonly SceneLoader sceneLoader;
		readonly ILogService log;
		readonly AssetProvider assetProvider;

		public GameplayState(ILoadingCurtain loadingCurtain,
			SceneLoader sceneLoader,
			ILogService log,
			AssetProvider assetProvider)
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