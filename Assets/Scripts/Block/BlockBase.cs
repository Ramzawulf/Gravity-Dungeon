using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Block
{
    public class BlockBase : MonoBehaviour
    {
        [Serializable]
        public class GizmoConfig
        {
            public Color NodeColor;
        }

        public static List<BlockBase> BlockList;

        public Transform[] MovementNodes;
        public GizmoConfig gizmoConfig;

        void OnEnable()
        {
            if (BlockList == null)
                BlockList = new List<BlockBase>();
            BlockList.Add(this);
        }

        void OnDisable()
        {
            BlockList.Remove(this);
        }

        private void OnDrawGizmos()
        {
            if (MovementNodes != null)
                for (int n = 0; n < MovementNodes.Length; n++)
                {
                    Gizmos.color = gizmoConfig.NodeColor;
                    Gizmos.DrawWireSphere(MovementNodes[n].position, 0.1f);
                }
        }
    }
}
