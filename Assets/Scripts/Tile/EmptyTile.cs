using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : Tile {

	private Collider col;

	public void Awake(){
		if (col == null)
			col = gameObject.GetComponent<Collider> ();
	}

	public override void OnStepIn ()
	{
		StartCoroutine (DropPlayer ());
	}

	private IEnumerator DropPlayer(){
		col.enabled = false;
		if (PlayerBehaviour.instance != null)
			StartCoroutine(PlayerBehaviour.instance.Fall ());
		yield return new WaitForSeconds(2.5f);
		col.enabled = true;
	}
}
