using Assets.Scripts.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Tile
{
    public class TileSwitch : Tile {

        public GameObject[] targets;

        public override void OnStepIn (GameObject go)
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
}