using Game.UI.Services.Factories;
using Game.UI.Services.PopUps;
using Game.UI.Services.Window;
using Zenject;

namespace Game.UI
{
	public class UIInstaller : Installer<UIInstaller>
	{
		public override void InstallBindings()
		{
			UIFactoryInstaller.Install(Container);

			Container.BindInterfacesAndSelfTo<WindowService>()
					 .AsSingle();

			Container.BindInterfacesAndSelfTo<PopUpService>()
					 .AsSingle();
		}
	}
}