using UI;
using UnityEngine;
using Core.Weapons;
using Core.Interactions;
using Setups.CharacterSetups;
using Core.AI.CharacterMovable;
using Core.AI.CharacterAttachment;
using Zenject;

namespace Core.AI.Workers
{
    public sealed class PlayerController : Character<PlayerSetup>
    {
        public IWeapon CurrentWeapon;

        [SerializeField] private Transform orientation;
        [SerializeField] public Transform attackPosition;
        [SerializeField] private HPViewController hpViewController;
        [Inject] private DiContainer _container;

        protected override void OnEnable()
        {
            base.OnEnable();
            OnChangeHp += hpViewController.SetProgress;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            OnChangeHp += hpViewController.SetProgress;
        }

        private void Awake()
        {
            Movable = new RigidbodyMovable(this, GetComponent<Rigidbody>());
            Attachment = new PlayerAttachment(this);
            WorkerStateMachine.Start(this);
            SessionManager.SetPlayer(this);
            CurrentWeapon = new GunWeapon(weaponSetup, attackPosition);
            _container.Inject(CurrentWeapon);
        }


        public override Vector3 Direction
        {
            get
            {
                var transformForward = orientation.forward;
                transformForward.y = 0;
                transformForward.Normalize();
                var transformRight = orientation.right;
                transformRight.y = 0;
                transformRight.Normalize();
                Vector3 direction = (transformForward * Joystick.SDirection.y +
                                     transformRight * Joystick.SDirection.x);

                direction = direction.normalized;
                return direction;
            }
        }

        private void FixedUpdate()
        {
            Movable.Move(this);
        }

        public override CharacterSetup GetCharacterSetup() => setup;

        public override void FindTargetToAttack(bool state)
        {
            hpViewController.ShowHideHP(!state);
            var enemy = SessionManager.GetEnemy();
            if (Attachment.Target == null && !state)
                Attachment.Target = enemy;
            else
                Attachment.Target = null;
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BaseInteraction interaction))
                interaction.OnEnter();
        }

        public override void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out BaseInteraction interaction))
                interaction.OnExit();
        }
    }
}