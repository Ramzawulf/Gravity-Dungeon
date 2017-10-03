using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraPositions : MonoBehaviour
    {

        public Transform FarBack;


        void Start()
        {
            UnityEngine.Camera.main.transform.SetParent(FarBack);
            UnityEngine.Camera.main.transform.localPosition = Vector3.zero;
        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnDrawGizmos()
        {
            if (FarBack != null)
                Gizmos.DrawWireSphere(FarBack.position, 0.1f);

        }
    }
}
