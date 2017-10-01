using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.V3
{
    [RequireComponent(typeof(ConstantForce))]
    [RequireComponent(typeof(Rigidbody))]
    public class PlayerBehaviour : MonoBehaviour
    {

        public float MovementSpeed = 5f;
        public Vector3 Gravity;
        public bool ControlsPaused = false;
        private Rigidbody myRigidbody;
        private ConstantForce myConsForce;

        private void Start()
        {
            myRigidbody = GetComponent<Rigidbody>();
            myConsForce = GetComponent<ConstantForce>();
            myConsForce.relativeForce = Gravity;
        }

        private void Update()
        {
            if (!ControlsPaused)
            {
                Act();
            }
        }

        private void OnDrawGizmos()
        {
            //Draw outline
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, new Vector3(1, 1, 1));

        }

        #region Movement

        private void Act()
        {
            if (Input.GetKey(KeyCode.F))
                FlipGravity();
            else if (Input.GetKey(KeyCode.A))
                FaceRight();
            else if (Input.GetKey(KeyCode.D))
                FaceLeft();
            else if (Input.GetKey(KeyCode.W))
                myRigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * MovementSpeed);
            else if (Input.GetKey(KeyCode.S))
                myRigidbody.MovePosition(transform.position - transform.forward * Time.deltaTime * MovementSpeed);

        }

        private void FaceLeft()
        {
            StartCoroutine(CR_FaceLeft());

        }

        private void FaceRight()
        {
            StartCoroutine(CR_FaceRight());
        }

        private void FlipGravity()
        {
            StartCoroutine(CR_FlipGravity());
        }

        #endregion

        #region CoRoutine

        private IEnumerator CR_FaceLeft()
        {
            yield return RotateMe(transform.up, 90, 3);

        }

        private IEnumerator CR_FaceRight()
        {
            yield return RotateMe(transform.up, -90, 3);
        }

        private IEnumerator RotateMe(Vector3 axis, float degrees, int strideSize = 5)
        {
            PauseControls();
            int stride;
            if (degrees > 0)
                stride = strideSize;
            else
                stride = -strideSize;
            Vector3 InitialPosition = transform.position;
            for (var i = 0; i < Mathf.Abs(degrees) / Math.Abs(strideSize); i++)
            {
                transform.RotateAround(transform.position, axis, stride);
                yield return null;

            }
            transform.position = InitialPosition;
            UnPauseControls();
        }

        private IEnumerator CR_FlipGravity()
        {
            PausePhysics();
            yield return RotateMe(transform.right, -90);
            yield return new WaitForSeconds(0.2f);
            UnPausePhysics();
        }

        #endregion

        #region Controls
        private void PausePhysics()
        {
            myConsForce.relativeForce = Vector3.zero;
            myRigidbody.velocity = Vector3.zero;
        }

        private void UnPausePhysics()
        {
            myConsForce.relativeForce = Gravity;
        }

        public void PauseControls()
        {
            ControlsPaused = true;
        }

        public void UnPauseControls()
        {
            ControlsPaused = false;
        }

        #endregion

    }
}