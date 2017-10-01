using UnityEngine;

namespace Assets
{
    [RequireComponent(typeof (Collider))]
    public class ColliderCheck : MonoBehaviour
    {
        public Collider Collider;
        private bool _isColliding;

        public bool IsColliding
        {
            get { return _isColliding; }
            set { }
        }

         void Awake()
        {
            Collider = GetComponent<Collider>();
            Collider.isTrigger = true;
        }

         void OnTriggerEnter(Collider col)
        {
            _isColliding = !col.CompareTag("Player");
        }

         void OnTriggerStay(Collider col)
        {
            _isColliding = !col.CompareTag("Player");
        }

         void OnTriggerExit(Collider other)
        {
            _isColliding = false;
        }

        public Vector3 Dimension()
        {
            return Collider.bounds.size;
        }
    }
}