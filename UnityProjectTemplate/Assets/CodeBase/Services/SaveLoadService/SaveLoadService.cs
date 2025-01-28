using CodeBase.Data;
using CodeBase.Services.PlayerProgressService;
using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.Services.SaveLoadService
{
	// This service implement saving and loading of progress.
	// There are for example it implemented by PlayerPrefs but you can implement you own variant of service.
	public class SaveLoadService : ISaveLoadService
	{
		const string ProgressKey = "Progress";

		readonly IEnumerable<IProgressSaver> saverServices;
		readonly IPersistentProgressService persistentProgressService;

		public SaveLoadService(IEnumerable<IProgressSaver> saverServices, IPersistentProgressService persistentProgressService)
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