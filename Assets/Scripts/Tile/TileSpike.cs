using Assets.Scripts.Tile.Traps;
using UnityEngine;

namespace Assets.Scripts.Tile
{
    public class TileSpike : Tile {
        public GameObject spikeGameObject;
        public float springSize;
        [SerializeField]
        public Spikes spikes;

        public void Awake(){
            type = TileType.Spike;
        }

        public override void OnStepIn (GameObject go)
        {
            //spikes.Spring ();
        }

    }
}
