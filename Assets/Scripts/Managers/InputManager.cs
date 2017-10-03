using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class InputManager : MonoBehaviour
    {
        public GameObject PointerSprite;

        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            RaycastHit hit;
            var isHit = PointerHit(out hit);
            if (isHit)
            {
                PointerSprite.transform.up = hit.normal;
                PointerSprite.transform.position = hit.point + hit.normal * 0.01f;
            }

            //Look for click
            if (Input.GetMouseButtonUp(0))
            {
                
            }

        }

        private bool PointerHit(out RaycastHit hit)
        {
            Vector3 VPmousePos = UnityEngine.Camera.main.ScreenToViewportPoint(Input.mousePosition);
            Ray ray = UnityEngine.Camera.main.ViewportPointToRay(VPmousePos);
            return Physics.Raycast(ray, out hit);
        }
    }
}
