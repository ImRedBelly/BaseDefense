using Core.AI.CharacterAttachment;
using UnityEngine;
using UnityEngine.AI;
using Setups.CharacterSetups;
using Core.AI.CharacterMovable;
using Core.Weapons;

namespace Core.AI.Workers
{
    public class EnemyController : Character<BaseEnemySetup>
    {
        public IWeapon CurrentWeapon;

        public override Vector3 Direction =>
            Attachment.Target == null ? Vector3.zero : Attachment.Target.transform.position;

        private void Awake()
        {
            WorkerStateMachine.Start(this);
            Movable = new EnemyMovable(this, GetComponentInChildren<NavMeshAgent>());
            Attachment = new EnemyAttachment(this);
        }

        protected override void Start()
        {
            base.Start();
            CurrentWeapon = new HandWeapon(this, weaponSetup);
            SessionManager.AddEnemy(this);
        }

        protected override void ChangeState(bool state)
        {
            base.ChangeState(state);
            FindTargetToAttack(state);
        }

        private void FixedUpdate()
        {
            Movable.Move(this);
        }

        public override CharacterSetup GetCharacterSetup() => setup;

        public override void FindTargetToAttack(bool state)
        {
            Attachment.Target = state ? null : Attachment.Target = SessionManager.Player;
        }
    }
}