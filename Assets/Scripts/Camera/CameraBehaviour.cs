using System;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.V3
{
    public class CameraBehaviour : MonoBehaviour
    {
        public Transform lookAt;
        public Transform camTransform;

        private UnityEngine.Camera cam;

        public float distance = 10.0f;
        public float currentX = 0.0f;
        public float currentY = 0.0f;

        private void Start()
        {
            camTransform = transform;
            cam = UnityEngine.Camera.main;
        }

        private void LateUpdate()
        {
            camTransform.LookAt(lookAt.position);
        }
    }
}
