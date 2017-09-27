using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.V2
{
    [RequireComponent(typeof (Animator))]
    [RequireComponent(typeof (PlayerChecks))]
    [RequireComponent(typeof (Collider))]
    [SuppressMessage("ReSharper", "SuggestVarOrType_BuiltInTypes")]
    public class PlayerMovement : MonoBehaviour
    {
        private Animator _anim;
        private PlayerChecks _checks;
        private Collider _col;
        public Text DebugWindow;
        public float GravityMagnitude = 2;
        [Tooltip("Meters per second")] public float MovementSped = 1;
        [Tooltip("Angles per second")] public float RotationSpeed = 60;
        private ShiftableGravity gravity;
        private Rigidbody _rb;

        public bool IsFrontalClear
        {
            get { return _checks.FrontalCheck.IsColliding; }
            set { }
        }

        public bool GotPerpendiculatTile
        {
            get { return _checks.PerpendicularCheck.IsColliding; }
            set { }
        }

        private void Awake()
        {
            _anim = GetComponent<Animator>();
            _checks = GetComponent<PlayerChecks>();
            _col = GetComponent<Collider>();
            _rb = GetComponent<Rigidbody>();
            gravity = new ShiftableGravity(Direction.Down, 9.8f);
        }

        private void FIxedUpdate()
        {
            ApplyGravity();
        }

        private void Update()
        {
            TryMove();
            Debug();
        }

        private void ApplyGravity()
        {
            _rb.AddForce(gravity.Vector);
        }

        private void TryMove()
        {
            var hInput = Input.GetAxisRaw("Horizontal");
            var vInput = Input.GetAxisRaw("Vertical");
            var rotation = Vector3.up*RotationSpeed*hInput*Time.deltaTime;
            transform.Rotate(rotation);
            var direction = Vector3.forward*MovementSped*vInput;
            print("D: " + direction);
            transform.Translate(direction*Time.deltaTime);
            _anim.SetFloat("speed", Mathf.Max(Math.Abs(hInput), Math.Abs(vInput)));
        }

        private void Debug()
        {
            var message = new StringBuilder();
            _col.enabled = false;
            message.AppendLine(string.Format("Frontal: {0}", IsFrontalClear));
            message.AppendLine(string.Format("Perpendicular: {0}", GotPerpendiculatTile));
            _col.enabled = true;

            DebugWindow.text = message.ToString();
        }
    }

    public class ShiftableGravity
    {
        public Direction Direction;
        public float Magnitude;

        public ShiftableGravity(Direction direction, float magnitude)
        {
            Magnitude = magnitude;
            Direction = direction;
        }

        public Vector3 Vector
        {
            get
            {
                switch (Direction)
                {
                    case Direction.Up:
                        return Vector3.up*Magnitude;
                    case Direction.Down:
                        return Vector3.down*Magnitude;
                    case Direction.North:
                        return Vector3.forward*Magnitude;
                    case Direction.South:
                        return Vector3.back*Magnitude;
                    case Direction.East:
                        return Vector3.right*Magnitude;
                    case Direction.West:
                        return Vector3.left*Magnitude;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            set { }
        }
    }

    public enum Direction
    {
        Up,
        Down,
        North,
        South,
        East,
        West
    }
}