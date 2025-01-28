using System.Collections;
using UnityEngine;

namespace Game.Infrastructure
{
	public interface ICoroutineRunner
	{
		Coroutine StartCoroutine(IEnumerator coroutine);
	}
}