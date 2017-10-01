using UnityEngine;

namespace Assets.Scripts.Tile
{
    public class TileManager : MonoBehaviour {

        public static TileManager instance;

        void Awake () {
            if (instance == null)
                instance = this;
            else if (instance != this)
                Destroy (gameObject);
        }
	
        // Update is called once per frame
        void Update () {
		
        }
    }
}
