using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSlippery : Tile {
	
	public override void OnStepIn (GameObject go)
	{
		StartCoroutine(PlayerBehaviour.instance.MoveTowards (Directions.Forward));
	}


}
