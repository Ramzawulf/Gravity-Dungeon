using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TileSwitch : Tile {

	public GameObject[] targets;

	public override void OnStepIn ()
	{
		foreach (var target in targets) {
			ISwitchTriggered s = target.GetComponent<ISwitchTriggered> ();
			if(s != null)
				s.OnSwitchOn ();
		}
	}

	public override void OnStepOut ()
	{
		foreach (var target in targets) {
			ISwitchTriggered s = target.GetComponent<ISwitchTriggered> ();
			if(s != null)
				s.OnSwitchOff ();
		}
	}
}