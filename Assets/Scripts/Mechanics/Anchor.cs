using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    [RequireComponent(typeof(ConstantForce))]
    [RequireComponent(typeof(Rigidbody))]
    public class Anchor : MonoBehaviour
    {

        public static List<Anchor> AnchorList;
        public float GravityCheck = 2f;
        private Rigidbody rb;
        private ConstantForce localGravity;


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
            rb = GetComponent<Rigidbody>();
            rb.angularDrag = 0;
            rb.drag = 0.95f;
            rb.useGravity = false;
            localGravity = GetComponent<ConstantForce>();
            localGravity.relativeForce = Vector3.zero;

        }

        void Start()
        {
            StartCoroutine(GravityRefresh());
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
                        Vector3 forceV = (attractor.transform.position - transform.position).normalized * attractor.AttractionForce;
                        print("Attractor pos: " + attractor.transform.position + "this pos: " + transform.position + "attr force = " + attractor.AttractionForce);
                        print("force vector = " + forceV);
                        resultingForce += forceV;
                    }
                    localGravity.force = resultingForce;
                    //rb.AddForce(resultingForce);
                }
                yield return new WaitForSeconds(GravityCheck);
            }
        }

        void Update()
        {

        }

        private void OnDrawGizmos()
        {
            if(Attractor.AttractorList != null)
            foreach (var att in Attractor.AttractorList)
            {
                if(att.InRange(this))
                        Gizmos.DrawLine(transform.position, att.transform.position);
            }
        }
        
    }
}
