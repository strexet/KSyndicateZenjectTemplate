using Game.UI.HUD;

namespace Game.Infrastructure.Factories
{
	public interface IGameFactory
	{
		IHUDRoot CreateHUD();

		void Cleanup();
	}
}