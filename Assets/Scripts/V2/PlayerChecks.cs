using UnityEngine;

namespace Assets.Scripts.V2
{
    [RequireComponent(typeof (PlayerMovement))]
    public class PlayerChecks : MonoBehaviour
    {
        //public ColliderCheck FloorCheck;
        public ColliderCheck FrontalCheck;
        public ColliderCheck PerpendicularCheck;

        private void Awake()
        {
        }

        private void Update()
        {
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(FrontalCheck.transform.position, FrontalCheck.Dimension());
            Gizmos.DrawWireCube(PerpendicularCheck.transform.position, PerpendicularCheck.Dimension());
        }
    }
}