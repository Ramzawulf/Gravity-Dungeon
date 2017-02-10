using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSlippery : Tile {
	public override void OnStepIn ()
	{
		StartCoroutine(PlayerBehaviour.instance.MoveTowards (Directions.Forward));
	}


}
