using Game.Data;

namespace Game.Services.SaveLoadService
{
	public interface ISaveLoadService
	{
		void SaveProgress();

		PlayerProgress LoadProgress();
	}
}