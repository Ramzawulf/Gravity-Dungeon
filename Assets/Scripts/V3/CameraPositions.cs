using UnityEngine;

namespace Assets.Scripts.V3
{
    public class CameraPositions : MonoBehaviour
    {

        private UnityEngine.Camera camera;
        public Transform FarBack;


        void Start()
        {
            camera = UnityEngine.Camera.main;
            camera.transform.SetParent(FarBack);
            camera.transform.position = Vector3.zero;
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
