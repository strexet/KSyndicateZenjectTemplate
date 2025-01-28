using Game.Data;
using Game.Services.PlayerProgressService;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Services.SaveLoadService
{
	// This service implement saving and loading of progress.
	// There are for example it implemented by PlayerPrefs but you can implement you own variant of service.
	public class SaveLoadService
	{
		const string ProgressKey = "Progress";

		readonly IEnumerable<IProgressSaver> saverServices;
		readonly PersistentProgressService persistentProgressService;

		public SaveLoadService(IEnumerable<IProgressSaver> saverServices, PersistentProgressService persistentProgressService)
		{
			this.saverServices = saverServices;
			this.persistentProgressService = persistentProgressService;
		}

		public void SaveProgress()
		{
			foreach (var saver in saverServices)
			{
				saver.UpdateProgress(persistentProgressService.Progress);
			}

			PlayerPrefs.SetString(ProgressKey, persistentProgressService.Progress.ToJson());
		}

		public PlayerProgress LoadProgress() => PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<PlayerProgress>();
	}
}