using UnityEngine;

namespace Game.UI.Elements
{
	public class Rotator : MonoBehaviour
	{
		[SerializeField] Vector3 rotationAxis;
		[SerializeField] float rotationSpeed;

		void Update() => transform.Rotate(rotationAxis, rotationSpeed * Time.deltaTime);
	}
}