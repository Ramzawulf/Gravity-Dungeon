using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpike : Tile {
	public GameObject spikeGameObject;
	public float springSize;
	private Spikes spikes;

	public void Awake(){
		spikes = spikeGameObject.GetComponent<Spikes> ();
		type = TileType.Spike;
	}

	public override void OnStepIn ()
	{
		spikes.Spring ();
	}

}
