using UnityEngine;

namespace Game.Infrastructure
{
	public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
	{
		void Awake() => DontDestroyOnLoad(this);
	}
}