using System.Collections.Generic;

namespace Game.Services.RandomizerService
{
	public static class RandomExtentions
	{
		public static TPayload GetRandomItem<TPayload>(this List<TPayload> list) =>
			list.Count == 0 ? default : list[UnityEngine.Random.Range(0, list.Count)];

		public static void Shuffle<TPayload>(this List<TPayload> list)
		{
			var listCount = list.Count;

			for (var i = 0; i < listCount - 1; i++)
			{
				var oldVal = list[i];
				var randomIndex = UnityEngine.Random.Range(i, listCount);
				list[i] = list[randomIndex];
				list[randomIndex] = oldVal;
			}
		}
	}
}