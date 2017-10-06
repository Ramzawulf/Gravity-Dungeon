using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    //[RequireComponent(typeof(ConstantForce))]
    //[RequireComponent(typeof(Rigidbody2D))]
    public class Anchor : MonoBehaviour
    {

        public static List<Anchor> AnchorList;
        public float GravityCheck = 0.25f;
        private Rigidbody2D rb;

        void OnEnable()
        {
            if (AnchorList == null)
                AnchorList = new List<Anchor>();
            AnchorList.Add(this);
        }

        void OnDisable()
        {
            AnchorList.Remove(this);
        }

        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.angularDrag = 0;
            // rb.drag = 0.25f;
        }

        void Start()
        {
            StartCoroutine(GravityRefresh());
        }

        private void OnCollisionEnter2D(Collision2D col)
        {
            
        }


        private IEnumerator GravityRefresh()
        {
            while (true)
            {
                //Reset Gravity
                Vector3 resultingForce = Vector3.zero;
                foreach (Attractor attractor in Attractor.AttractorList)
                {

                    if (attractor.InRange(this))
                    {
                        Vector3 forceV = (attractor.transform.position - transform.position).normalized * attractor.ForceByDistance(this);
                        print("Attractor pos: " + attractor.transform.position + "this pos: " + transform.position + "attr force = " + attractor.AttractionForce);
                        print("force vector = " + forceV);
                        resultingForce += forceV;
                    }
                    rb.velocity = resultingForce;
                }
                yield return new WaitForSeconds(GravityCheck);
            }
        }

        private void OnDrawGizmos()
        {
            if (Attractor.AttractorList != null)
                foreach (var att in Attractor.AttractorList)
                {
                    if (att.InRange(this))
                        Gizmos.DrawLine(transform.position, att.transform.position);
                }
        }

    }
}
