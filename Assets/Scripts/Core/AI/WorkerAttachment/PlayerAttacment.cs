using Core.AI.Workers;
using Library;
using UnityEngine;

namespace Core.AI.WorkerAttachment
{
    public class PlayerAttacment : Attachment
    {
        private Character character;

        public PlayerAttacment(Character character)
        {
            this.character = character;
        }

        public override void Attack()
        {
            if (Target == null)
                character.FindTargetToAttack();
            character.Animator.SetTrigger(AnimationsPrefsNames.Attack);
            Debug.LogError("AAAATAAACK");
        }
    }
}