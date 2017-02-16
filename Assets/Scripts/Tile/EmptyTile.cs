using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTile : Tile {

	private Collider col;

	public void Awake(){
		if (col == null)
			col = gameObject.GetComponent<Collider> ();
	}

	public override void OnStepIn (GameObject go)
	{
		onTile = go;
		StartCoroutine (Drop ());
	}

	private IEnumerator Drop(){
		col.enabled = false;
		if (onTile.GetComponent<PlayerBehaviour> () != null)
			StartCoroutine (onTile.GetComponent<PlayerBehaviour> ().Fall ());
		else {
			//Drop other elements
		}

		yield return new WaitForSeconds(2.5f);
		col.enabled = true;
	}
}
