using CodeBase.Infrastructure.AssetManagement;
using CodeBase.UI.PopUps.ErrorPopup;
using CodeBase.UI.PopUps.PolicyAcceptPopup;
using Cysharp.Threading.Tasks;
using Zenject;

namespace CodeBase.UI.Services.Factories
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