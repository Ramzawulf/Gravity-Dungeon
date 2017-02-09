using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileSwitch : Tile {

	public UnityEvent switchEvent;

	public override void OnStepIn ()
	{
		if (switchEvent != null)
			switchEvent.Invoke ();
	}

	public override void OnStepOut ()
	{
		if (switchEvent != null)
			switchEvent.Invoke ();
	}
}