using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Block;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Mechanics
{
    public class Anchor : MonoBehaviour
    {

        public static List<Anchor> AnchorList;
        public float HearthBeat = 0.25f;
        public Transform Direction;
        public Vector3 GravityPull;
        public Plane myPlane;


        void Awake()
        {
            myPlane = new Plane(transform.up, transform.position);
        }

        public IEnumerator Move(Vector3 target)
        {
            transform.LookAt(target);
            while (true)
            {
                var t = Vector3.MoveTowards(transform.position, target, Time.deltaTime);
                transform.position = t;
                yield return null;
                if (target == transform.position)
                {
                    Transform n = GetNearestNode(GetDirection());
                    target = n.position;
                }
                yield return null;
            }

        }

        void Start()
        {
            StartCoroutine(Move(transform.position));
        }

        void Update()
        {

        }


        private Transform GetNearestNode(Vector3 pos)
        {
            float minDistance = float.MaxValue;
            Transform node = null;
            foreach (BlockBase block in BlockBase.BlockList)
            {
                foreach (Transform movementNode in block.MovementNodes)
                {
                    if (Vector3.Distance(pos, movementNode.position) < minDistance && Vector3.Distance(movementNode.position, transform.position) > 0.5f)
                    {
                        node = movementNode;
                        minDistance = Vector3.Distance(pos, movementNode.position);
                    }
                }
            }

            return node;
        }

        private Vector3 GetDirection()
        {
            Vector3 result = transform.position;

            foreach (Attractor attractor in Attractor.AttractorList)
            {
                result += (attractor.transform.position - transform.position).normalized;
            }

            myPlane.SetNormalAndPosition(transform.up, transform.position + transform.up * 0.5f);

            float distanceToIntersection;
            Ray intersectionRay = new Ray(result, transform.up);
            

            if (myPlane.Raycast(intersectionRay, out distanceToIntersection))
            {
                Vector3 hit = intersectionRay.GetPoint(distanceToIntersection);
                return hit;
            }
            return result.normalized;
        }

        #region To Be Updated

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
        #endregion

        #region Debug
        private void OnDrawGizmos()
        {
            DebugNearestNode();
        }

        private void DebugNearestNode()
        {
            if (Attractor.AttractorList != null)
            {
                Vector3 d = GetDirection();
                Transform n = GetNearestNode(d);

                Gizmos.color = Color.blue;
                Gizmos.DrawWireCube(d, new Vector3(0.1f, 0.1f, 0.1f));

                Gizmos.color = Color.red;
                if (n != null)
                    Gizmos.DrawWireCube(n.position, new Vector3(0.2f, 0.2f, 0.2f));
            }
        }
        #endregion
    }
}
