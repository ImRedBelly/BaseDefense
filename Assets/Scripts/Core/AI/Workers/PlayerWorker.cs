using UnityEngine;
using Core.AI.WorkersMovable;

namespace Core.AI.Workers
{
    public class PlayerWorker : Worker
    {
        [SerializeField] private Transform orientation;
        protected virtual void Awake() => Movable =
            new RigidbodyMovable(this, GetComponent<Rigidbody>());

        public override Vector3 Direction
        {
            get
            {
                var transformForward = orientation.forward;
                transformForward.y = 0;
                transformForward.Normalize();
                var transformRight = orientation.right;
                transformRight.y = 0;
                transformRight.Normalize();
                Vector3 direction = (transformForward * Joystick.SDirection.y +
                                     transformRight * Joystick.SDirection.x);

                direction = direction.normalized;
                return direction;
            }
        }

        private void FixedUpdate()
        {
            Movable.Move(this);
        }
    }
}