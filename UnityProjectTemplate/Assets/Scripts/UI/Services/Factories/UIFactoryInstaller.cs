using Cysharp.Threading.Tasks;
using Game.Infrastructure.AssetManagement;
using Game.UI.PopUps.ErrorPopup;
using Game.UI.PopUps.PolicyAcceptPopup;
using Zenject;

namespace Game.UI.Services.Factories
{
	/// <summary>
	///     Bind ui factories here
	/// </summary>
	public class UIFactoryInstaller : Installer<UIFactoryInstaller>
	{
		public override void InstallBindings()
		{

			Container.Bind<IUIFactory>().To<UIFactory>()
					 .AsSingle();

			// Eexample of binding zenject factories in async
			Container.BindFactory<string, UniTask<PolicyAcceptPopup>, PolicyAcceptPopup.Factory>()
					 .FromFactory<PrefabFactoryAsync<PolicyAcceptPopup>>();

			Container.BindFactory<string, UniTask<ErrorPopup>, ErrorPopup.Factory>()
					 .FromFactory<PrefabFactoryAsync<ErrorPopup>>();
		}
	}
}