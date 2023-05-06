using Lean.Pool;
using UnityEngine;
using Core.Weapons;
using UnityEngine.AI;
using Setups.CharacterSetups;
using Core.AI.CharacterMovable;
using Core.AI.CharacterAttachment;

namespace Core.AI.Characters
{
    public class EnemyController : Character<BaseEnemySetup>
    {
        public IWeapon CurrentWeapon;

        public override Vector3 Direction =>
            Attachment.Target == null
                ? Attachment.DefaultTarget.position
                : Attachment.Target.transform.position;


        public override CharacterSetup GetCharacterSetup() => setup;

        public override void FindTargetToAttack(bool state)
        {
            Attachment.Target = state ? null : Attachment.Target = SessionManager.Player;
        }

        public float GetDistanceToPlayer(Vector3 positionPlayer)
        {
            return Vector3.Distance(transform.position, positionPlayer);
        }

        public override void Initialize()
        {
            base.Initialize();
            SessionManager.AddEnemy(this);
            CurrentWeapon = new HandWeapon(this, weaponSetup);
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
            Attachment.Target = null;
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
            var value = Random.Range(0, 100);
            if (value < setup.chanceSpawnReward)
            {
                var currentPosition = transform.position;
                var reward = LeanPool.Spawn(setup.rewardObject, new Vector3(currentPosition.x, 0, currentPosition.z),
                    Quaternion.identity);
                Container.Inject(reward);
                reward.Initialize();
            }

            SessionManager.RemoveEnemy(this);
            LeanPool.Despawn(this);
        }
    }
}