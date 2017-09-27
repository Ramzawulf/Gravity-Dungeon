using System;
using UnityEngine;

namespace Assets.Scripts.V2
{
    [Serializable]
    public class RayCheck
    {
        public Transform End;
        public Transform Start;

        public bool CollisionExists()
        {
            return Physics.Raycast(Start.position, End.position, Vector3.Distance(Start.position, End.position));
        }
    }
}