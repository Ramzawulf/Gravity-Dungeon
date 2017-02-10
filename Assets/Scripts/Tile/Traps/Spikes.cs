using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour, ISpringTrap {
	public float springLength = 0.5f;
	public float springSpeed = 0.5f;
	private Vector3 originalPosition;
	private Vector3 sprungPosition;

	public void Awake(){
		originalPosition = transform.position;
		sprungPosition = originalPosition + Vector3.up * springLength;
	}

	#region ISpringTrap implementation
	public void Spring ()
	{
		StartCoroutine (Activate ());
	}
	#endregion

	private IEnumerator Activate(){
		while (true) {
			transform.position = Vector3.MoveTowards (transform.position, sprungPosition, springSpeed);
			if (transform.position == sprungPosition)
				break;
			yield return null;
		}
		while (true) {
			transform.position = Vector3.MoveTowards (transform.position, originalPosition, springSpeed);
			if (originalPosition == transform.position)
				break;
			yield return null;
		}
	}
}
