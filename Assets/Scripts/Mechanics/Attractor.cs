using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    public class Attractor : MonoBehaviour
    {

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

        void Start()
        {
         
        }

     
        void Update()
        {

        }

        public bool InRange(Anchor anchor)
        {
            return Vector3.Distance(transform.position, anchor.transform.position) <= Range;
        }
    }
}
