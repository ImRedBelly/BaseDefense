using Core.AI.Characters;
using Library;
using UnityEngine;

namespace Core.StateMachine.States
{
    public class AttackState : BaseState
    {
        private float _interactionTime;


        public override void OnEnter(Character character)
        {
            base.OnEnter(character);
            Character.Animator.SetBool(AnimationsPrefsNames.IsWeapon, true);
        }


        public override void Update()
        {
            _interactionTime += Time.deltaTime;

            if (_interactionTime > Character.GetCharacterSetup().attackTime)
                _interactionTime = Character.GetCharacterSetup().attackTime;

            var progress = _interactionTime / Character.GetCharacterSetup().attackTime;

            if (progress >= 1)
            {
                Character.Attachment.Attack();
                _interactionTime = 0;
            }
        }

        public override void OnExit(Character character)
        {
            character.Animator.SetBool(AnimationsPrefsNames.IsWeapon, false);
            base.OnExit(character);
        }
    }
}