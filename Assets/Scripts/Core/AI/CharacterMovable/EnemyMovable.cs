using Library;
using UnityEngine;
using UnityEngine.AI;
using Core.AI.Workers;

namespace Core.AI.CharacterMovable
{
    public class EnemyMovable : Movable
    {
        public override bool IsStop => _agent.velocity.normalized.magnitude == 0;
        private NavMeshAgent _agent;

        public EnemyMovable(Character character, NavMeshAgent agent)
        {
            _agent = agent;
            _agent.speed = character.GetCharacterSetup().speedMove;
        }


        public override void Move(Character character)
        {
            var speed = _agent.velocity.normalized.magnitude;
            character.Animator.SetFloat(AnimationsPrefsNames.Speed, Mathf.Round(speed));

            if (character.Direction == Vector3.zero) return;
            if (!EndPath(character.transform, character.Attachment.Target.transform,
                character.GetCharacterSetup().attackDistance))
                _agent.SetDestination(character.Direction);
        }

        private bool EndPath(Transform startPoint, Transform target, float distanceAttack)
        {
            float distance = Vector3.Distance(startPoint.position, target.position);
            bool isStopped = distance < distanceAttack;
            _agent.isStopped = isStopped;
            return isStopped;
        }
    }
}