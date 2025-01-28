using Game.UI.HUD;

namespace Game.Infrastructure.Factories
{
	public class GameFactory
	{
		readonly HUDRoot.Factory hudFactory;

		public GameFactory(HUDRoot.Factory hudFactory) => this.hudFactory = hudFactory;

		public HUDRoot CreateHUD() => hudFactory.Create();

		public void Cleanup() { }
	}
}