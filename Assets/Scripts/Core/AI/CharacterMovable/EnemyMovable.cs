using Core.AI.Characters;
using Library;
using UnityEngine;
using UnityEngine.AI;

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

            if (!EndPath(character.transform.position, character.Direction,
                character.GetCharacterSetup().attackDistance))
                _agent.SetDestination(character.Direction);
        }

        private bool EndPath(Vector3 startPoint, Vector3 target, float distanceAttack)
        {
            float distance = Vector3.Distance(startPoint, target);
            bool isStopped = distance < distanceAttack;
            _agent.isStopped = isStopped;
            return isStopped;
        }
    }
}