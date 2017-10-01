using Assets.Scripts.V1;
using Assets.Scripts.V2;
using UnityEngine;

namespace Assets.Scripts.Tile
{
    public class TileSlippery : global::Assets.Scripts.Tile.Tile {
	
        public override void OnStepIn (GameObject go)
        {
            StartCoroutine(PlayerBehaviour.instance.MoveTowards (Directions.Forward));
        }


    }
}
