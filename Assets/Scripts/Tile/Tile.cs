using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

	public Transform centre;
	public TileType type;
	private Platform platform;

	void Start () {
		/*var parent = transform.parent;
		platform = parent.GetComponent<Platform> ();
		if (platform == null)
			Destroy (gameObject);*/
		
	}


	public virtual void OnStepIn ()
	{
		print ("steped into the tile " + gameObject.name);

	}

	public virtual void OnStepOut ()
	{
		print ("steped out of the tile " + gameObject.name);
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
