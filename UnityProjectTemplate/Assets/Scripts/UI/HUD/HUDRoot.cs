using UnityEngine;
using Zenject;

namespace Game.UI.HUD
{
	public class HUDRoot : MonoBehaviour
	{
		public class Factory : PlaceholderFactory<HUDRoot> { }
	}
}