using UnityEngine;

namespace Assets.Scripts.Camera
{
    public class CameraBehaviour : MonoBehaviour
    {
        public Transform lookAt;

        private void LateUpdate()
        {
            transform.LookAt(lookAt.position);
        }
    }
}
