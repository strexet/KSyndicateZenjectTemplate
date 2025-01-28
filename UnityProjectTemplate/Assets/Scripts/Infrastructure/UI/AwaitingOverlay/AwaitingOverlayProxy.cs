using CodeBase.Infrastructure;
using Cysharp.Threading.Tasks;

namespace CodeBase.UI.Overlays
{
	public class AwaitingOverlayProxy : IAwaitingOverlay
	{
		readonly AwaitingOverlay.Factory factory;
		IAwaitingOverlay impl;

		public AwaitingOverlayProxy(AwaitingOverlay.Factory factory) =>
			this.factory = factory;

		public void Show(string withMessage) => impl.Show(withMessage);

		public void Hide() => impl.Hide();

		public async UniTask InitializeAsync() =>
			impl = await factory.Create(InfrastructureAssetPath.AwaitingOverlay);
	}
}