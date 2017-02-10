using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public Transform centre;
	public TileType type;

	void Start () {
	}

	public virtual void OnStepIn ()
	{
	}

	public virtual void OnStepOut ()
	{
	}

	public override string ToString ()
	{
		return gameObject.name;
	}

	void OnDrawGizmos(){
		if (centre == null)
			return;
		Gizmos.color = Color.cyan;
		Gizmos.DrawLine (transform.position, centre.position);
		Gizmos.DrawWireSphere (centre.position, 0.05f);

	}
}
