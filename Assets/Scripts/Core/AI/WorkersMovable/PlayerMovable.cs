using Core.AI.Workers;
using Library;
using UnityEngine;

namespace Core.AI.WorkersMovable
{
    public class RigidbodyMovable : Movable
    {
        private  Worker _worker;

        private Rigidbody _rigidbody;
        public RigidbodyMovable(Worker worker, Rigidbody rigidbody) 
        {
            _worker = worker;
            _rigidbody = rigidbody;
        }

        public override float Speed => _rigidbody.velocity.magnitude;
        public override bool IsStop => _worker.Direction == Vector3.zero;
        public override void Move(Worker worker)
        {
            var normalizedDirection = worker.Direction;
            var moveDirection = normalizedDirection * 6;
             moveDirection.y = _rigidbody.velocity.y;
            _rigidbody.velocity = moveDirection;
            worker.Animator.SetFloat(AnimationsPrefsNames.Speed, Mathf.Round(Speed));

            if (normalizedDirection != Vector3.zero)
            {
                 _worker.transform.rotation = Quaternion.LookRotation(normalizedDirection);
            }
        }
    }
}