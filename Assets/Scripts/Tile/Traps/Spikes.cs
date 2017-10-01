using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Tile.Traps
{
    public class Spikes : MonoBehaviour, ISpringTrap {
        public float springLength = 0.5f;
        public float springSpeed = 0.5f;
        public Transform spikeGameObject;
        public Transform sprungPosition;

        public void Awake(){
		
        }

        #region ISpringTrap implementation
        public void Spring (float timer = 0)
        {
            StartCoroutine (Activate (timer));
        }
        #endregion

        private IEnumerator Activate(float timer){
            //play sound
            yield return new WaitForSeconds (timer);

            while (Vector3.Distance(spikeGameObject.transform.position ,sprungPosition.position) > float.Epsilon) {
                spikeGameObject.position = Vector3.MoveTowards (spikeGameObject.position, sprungPosition.position, springSpeed * Time.deltaTime);
                yield return null;
            }

            yield return new WaitForSeconds (0.75f);

            while (Vector3.Distance(spikeGameObject.transform.position, transform.position) > float.Epsilon) {
                spikeGameObject.position = Vector3.MoveTowards (spikeGameObject.position, transform.position, springSpeed * Time.deltaTime * 0.5f);
                yield return null;
            }

        }

        public void OnDrawGizmos(){
            Gizmos.color = Color.red;
            Gizmos.DrawLine (transform.position, sprungPosition.position);
            //Gizmos.DrawWireSphere (sprungPosition.position, 0.01f);
            Gizmos.DrawWireCube (sprungPosition.position, spikeGameObject.transform.lossyScale);
        }

        public void Update(){
            if (Input.GetKeyDown (KeyCode.T)) {
                Spring (1.5f);
            }
			
        }
    }
}
