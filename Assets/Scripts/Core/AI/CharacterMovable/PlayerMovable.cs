using Core.AI.Workers;
using Library;
using UnityEngine;

namespace Core.AI.CharacterMovable
{
    public class RigidbodyMovable : Movable
    {
        public override bool IsStop => character.Direction == Vector3.zero;
        private Character character;
        private Rigidbody _rigidbody;

        public RigidbodyMovable(Character character, Rigidbody rigidbody)
        {
            this.character = character;
            _rigidbody = rigidbody;
        }


        public override void Move(Character character)
        {
            var normalizedDirection = character.Direction;
            var moveDirection = normalizedDirection * character.GetCharacterSetup().speedMove;
            moveDirection.y = _rigidbody.velocity.y;
            _rigidbody.velocity = moveDirection;
            character.Animator.SetFloat(AnimationsPrefsNames.Speed, Mathf.Round(_rigidbody.velocity.magnitude));


            if (character.Attachment.Target != null)
            {
                var direction = character.Attachment.Target.transform.position - character.transform.position;
                var directionView = new Vector3(direction.x, 0, direction.z);
                this.character.transform.rotation = Quaternion.LookRotation(directionView);
            }
            else if (normalizedDirection != Vector3.zero)
                this.character.transform.rotation = Quaternion.LookRotation(normalizedDirection);
        }
    }
}