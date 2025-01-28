using Cysharp.Threading.Tasks;

namespace Game.Infrastructure.UI.LoadingCurtain
{
	public class LoadingCurtainProxy : ILoadingCurtain
	{
		readonly LoadingCurtain.Factory factory;
		ILoadingCurtain impl;

		public LoadingCurtainProxy(LoadingCurtain.Factory factory) =>
			this.factory = factory;

		public void Show() => impl.Show();

		public void Hide() => impl.Hide();

		public async UniTask InitializeAsync() =>
			impl = await factory.Create(InfrastructureAssetPath.CurtainPath);
	}
}