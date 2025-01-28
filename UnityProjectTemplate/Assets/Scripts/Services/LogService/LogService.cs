using UnityEngine;

namespace CodeBase.Services.LogService
{
	public class LogService : ILogService
	{
		const bool IsLogEnabled = true;

		public void Log(string msg)
		{
			if (IsLogEnabled)
			{
				Debug.Log(msg);
			}
		}

		public void LogError(string msg)
		{
			if (IsLogEnabled)
			{
				Debug.LogError(msg);
			}
		}

		public void LogWarning(string msg)
		{
			if (IsLogEnabled)
			{
				Debug.LogWarning(msg);
			}
		}
	}
}