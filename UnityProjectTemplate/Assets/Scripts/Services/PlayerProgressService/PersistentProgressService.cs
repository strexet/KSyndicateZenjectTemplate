namespace Game.Services.PlayerProgressService
{
	public class PersistentProgressService : IPersistentProgressService
	{
		public Data.PlayerProgress Progress { get; set; }
	}
}