using Core.AI.Workers;
using Library;
using UnityEngine;

namespace Core.AI.WorkersMovable
{
    public class RigidbodyMovable : Movable
    {
        private Character character;

        private Rigidbody _rigidbody;

        public RigidbodyMovable(Character character, Rigidbody rigidbody)
        {
            this.character = character;
            _rigidbody = rigidbody;
        }

        public override bool IsStop => character.Direction == Vector3.zero;

        public override void Move(Character character)
        {
            var normalizedDirection = character.Direction;
            var moveDirection = normalizedDirection * 6;
            moveDirection.y = _rigidbody.velocity.y;
            _rigidbody.velocity = moveDirection;
            character.Animator.SetFloat(AnimationsPrefsNames.Speed, Mathf.Round(_rigidbody.velocity.magnitude));

            if (character.Attachment.Target != null)
                this.character.transform.rotation = Quaternion.LookRotation(
                    character.Attachment.Target.transform.position
                    - character.transform.position);
            else if (normalizedDirection != Vector3.zero)
                this.character.transform.rotation = Quaternion.LookRotation(normalizedDirection);
        }
    }
}