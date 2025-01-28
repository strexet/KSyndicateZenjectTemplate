using Game.UI.HUD;

namespace Game.Infrastructure.Factories
{
	public class GameFactory : IGameFactory
	{
		readonly HUDRoot.Factory hudFactory;

		public GameFactory(HUDRoot.Factory hudFactory) => this.hudFactory = hudFactory;

		public IHUDRoot CreateHUD() => hudFactory.Create();

		public void Cleanup() { }
	}
}