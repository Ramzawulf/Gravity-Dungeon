using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

	[SerializeField]
	public Directions orientation {
		get {
			return GetOrientation();
		}
		set {
			SetOrientation(value);
		}
	}

	// Use this for initialization
	void Start () {
		gameObject.name = "Facing + " + orientation;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private Directions GetOrientation(){
	
		if (gameObject.transform.up == Vector3.up)
			return	Directions.Up;
		if (gameObject.transform.up == Vector3.down)
			return	Directions.Down;
		if (gameObject.transform.up == Vector3.left)
			return	Directions.Left;
		if (gameObject.transform.up == Vector3.right)
			return	Directions.Right;
		if (gameObject.transform.up == Vector3.forward)
			return	Directions.Forward;
		if (gameObject.transform.up == Vector3.back)
			return	Directions.Back;
		return Directions.Undefined;
		
	}

	private void SetOrientation(Directions orientation){
		switch (orientation) {
			case Directions.Up:
				gameObject.transform.up = Vector3.up;
				break;
			case Directions.Down:
				gameObject.transform.up = Vector3.down;
				break;
			case Directions.Left:
				gameObject.transform.up = Vector3.left;
				break;
			case Directions.Right:
				gameObject.transform.up = Vector3.right;
				break;
			case Directions.Forward:
				gameObject.transform.up = Vector3.fwd;
				break;
			case Directions.Back:
				gameObject.transform.up = Vector3.back;
				break;
		}
	}
}
