using UnityEngine;
using Zenject;

namespace Game.UI.HUD
{
	public class HUDRoot : MonoBehaviour, IHUDRoot
	{
		public class Factory : PlaceholderFactory<HUDRoot> { }
	}
}