using System;
using Assets.Scripts.Utils;
using UnityEngine;

namespace Assets.Scripts
{
    public class StageTerrain : Singleton<StageTerrain>
    {
        [Serializable]
        public class GizmoConfig
        {
        }
        public Vector3 Center { get { return transform.position; } private set { transform.position = value; } }
        public GizmoConfig gizConfig;
        public float MechanicRadius = 5f;
        public float GeometryRadius = 4f;

        void Start()
        {

        }


        void Update()
        {

        }

        private void OnDrawGizmos()
        {

        }
    }
}
