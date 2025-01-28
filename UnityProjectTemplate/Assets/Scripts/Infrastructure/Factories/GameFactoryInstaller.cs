using Game.UI.HUD;
using Zenject;

namespace Game.Infrastructure.Factories
{
	/// <summary>
	///     Bind game sub-factories here.
	/// </summary>
	public class GameFactoryInstaller : Installer<GameFactoryInstaller>
	{
		public override void InstallBindings()
		{
			Container.BindFactory<HUDRoot, HUDRoot.Factory>()
					 .FromComponentInNewPrefabResource(InfrastructureAssetPath.HUDRoot);

			Container.Bind<GameFactory>()
					 .To<GameFactory>()
					 .AsSingle();
		}
	}
}