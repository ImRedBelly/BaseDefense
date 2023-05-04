using Core.AI.WorkerAttachment;
using Core.AI.Workers;
using Library;
using UnityEngine;

namespace Core.AI.CharacterAttachment
{
    public class EnemyAttachment : Attachment
    {
        private EnemyController _character;

        public EnemyAttachment(Character character)
        {
            _character = (EnemyController) character;
        }

        public override void Attack()
        {
            if (Target == null)
                _character.FindTargetToAttack(false);

            if (PlayerClothes(_character.transform, _character.Attachment.Target.transform,
                _character.GetCharacterSetup().attackDistance))
                _character.CurrentWeapon.Attack();
        }

        private bool PlayerClothes(Transform startPoint, Transform target, float distanceAttack)
        {
            float distance = Vector3.Distance(startPoint.position, target.position);
            bool isStopped = distance < distanceAttack;
            return isStopped;
        }
    }
}