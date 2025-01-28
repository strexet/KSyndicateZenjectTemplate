using UnityEngine;

namespace CodeBase.Infrastructure
{
	public class CoroutineRunner : MonoBehaviour, ICoroutineRunner
	{
		void Awake() => DontDestroyOnLoad(this);
	}
}