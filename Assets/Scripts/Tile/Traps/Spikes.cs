using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour, ISpringTrap {
	public float springLength = 0.5f;
	public float springSpeed = 0.5f;
	public GameObject spikes;
	public Transform sprungPosition;

	public void Awake(){
		
	}

	#region ISpringTrap implementation
	public void Spring ()
	{
		StartCoroutine (Activate ());
	}
	#endregion

	private IEnumerator Activate(){
		print ("sprung!!!");
		/*
		spikes.transform.position = sprungPosition.position;

		yield return new WaitForSeconds (0.8f);
		spikes.transform.position = transform.position;
		*/


		while (true) {
			spikes.transform.position = Vector3.MoveTowards (spikes.transform.position, sprungPosition.position, springSpeed);
			if (spikes.transform.position == sprungPosition.position)
				break;
			yield return null;
		}
		while (true) {
			spikes.transform.position = Vector3.MoveTowards (spikes.transform.position, transform.position, springSpeed);
			yield return null;
			if (spikes.transform.position == transform.position)
				break;
		}
	}

	public void OnDrawGizmos(){
		Gizmos.color = Color.red;
		Gizmos.DrawLine (transform.position, sprungPosition.position);
		//Gizmos.DrawWireSphere (sprungPosition.position, 0.01f);
		Gizmos.DrawWireCube (sprungPosition.position, spikes.transform.lossyScale);
	}

	public void Update(){
		if (Input.GetKeyDown (KeyCode.T)) {
			Spring ();
		}
			
	}
}
