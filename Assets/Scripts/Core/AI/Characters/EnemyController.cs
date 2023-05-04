using Core.AI.CharacterAttachment;
using Core.AI.CharacterMovable;
using Core.Weapons;
using Lean.Pool;
using Setups.CharacterSetups;
using UnityEngine;
using UnityEngine.AI;

namespace Core.AI.Characters
{
    public class EnemyController : Character<BaseEnemySetup>
    {
        public float DistanceToPlayer => (Direction - transform.position).magnitude;
        public IWeapon CurrentWeapon;

        public override Vector3 Direction =>
            Attachment.Target == null ? Attachment.DefaultTarget.position
                : Attachment.Target.transform.position;


        public override CharacterSetup GetCharacterSetup() => setup;

        public override void FindTargetToAttack(bool state)
        {
            Attachment.Target = state ? null : Attachment.Target = SessionManager.Player;
        }

        protected override void Start()
        {
            base.Start();
            CurrentWeapon = new HandWeapon(this, weaponSetup);
            SessionManager.AddEnemy(this);
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            OnDestroyCharacter += DestroyCharacter;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            OnDestroyCharacter -= DestroyCharacter;
        }


        protected override void ChangeState(bool state)
        {
            base.ChangeState(state);
            FindTargetToAttack(state);
        }

        private void Awake()
        {
            WorkerStateMachine.Start(this);
            Movable = new EnemyMovable(this, GetComponentInChildren<NavMeshAgent>());
            Attachment = new EnemyAttachment(this);
        }

        private void FixedUpdate()
        {
            Movable.Move(this);
        }

        private void DestroyCharacter()
        {
            SessionManager.RemoveEnemy(this);
            LeanPool.Despawn(this);
        }
    }
}