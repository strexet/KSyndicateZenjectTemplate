using Game.Services.LogService;
using System;

namespace Game.Services.AdsService
{
	public class AdsService
	{
		readonly ILogService log;

		public AdsService(ILogService log) => this.log = log;

		public event Action RewardedVideoReady;

		public bool IsRewardedVideoReady { get; }

		public void Initialize()
		{
			log.LogWarning("Initialization of ads service isn't implemented yet");
		}

		public void ShowRewardedVideo(Action onVideoFinished)
		{
			log.LogWarning("Showing of ads isn't implemented yet");
		}
	}
}