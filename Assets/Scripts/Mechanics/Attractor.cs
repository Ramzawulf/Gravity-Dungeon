using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    public class Attractor : MonoBehaviour
    {
        public enum AttractorForce
        {
            low,
            medium,
            high
        }

        public static List<Attractor> AttractorList;
        public float AttractionForce = 10f;
        public float Range = 15f;

        void OnEnable()
        {
            if (AttractorList == null)
                AttractorList = new List<Attractor>();
            AttractorList.Add(this);
        }

        void OnDisable()
        {
            AttractorList.Remove(this);
        }



        #region To Adapt
        public bool InRange(Anchor anchor)
        {
            return Vector3.Distance(transform.position, anchor.transform.position) <= Range;
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.CompareTag("Anchor"))
                Destroy(gameObject);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            // Gizmos.DrawWireSphere(transform.position, Range);
        }

        public float ForceByDistance(Anchor anchor)
        {
            float dist = Mathf.Lerp(0, Range, Vector3.Distance(transform.position, anchor.transform.position));
            return dist * AttractionForce;
        }
        #endregion

        public Vector3 GravityPull(Anchor anchor)
        {
            return ((anchor.transform.position - transform.position) * AttractionForce);
        }
    }
}
