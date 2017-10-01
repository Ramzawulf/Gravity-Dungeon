using UnityEngine;

namespace Assets.Scripts.UI
{
    public class LookAtCamera : MonoBehaviour
    {

        public UnityEngine.Camera Cam;

        private void Update()
        {
            transform.LookAt(transform.position + Cam.transform.rotation * Vector3.forward,
            Cam.transform.rotation * Vector3.up);
        }
    }
}
