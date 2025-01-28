using UnityEngine.UI;

namespace Game.UI.Extensions
{
	public static class ButtonExtensions
	{
		public static ButtonAwaiter GetAwaiter(this Button button) => new(button);
	}
}