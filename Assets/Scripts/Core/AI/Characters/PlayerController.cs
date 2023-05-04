using System.Collections.Generic;
using Core.AI.CharacterAttachment;
using Core.AI.CharacterMovable;
using Core.Interactions;
using Core.Weapons;
using Setups.CharacterSetups;
using UI;
using UnityEngine;
using Zenject;

namespace Core.AI.Characters
{
    public sealed class PlayerController : Character<PlayerSetup>
    {
        public IWeapon CurrentWeapon;

        [SerializeField] private Transform orientation;
        [SerializeField] private Transform attackPosition;
        [SerializeField] private HpViewController hpViewController;

        [Inject] private DiContainer _container;
        private List<BaseInteraction> _interactions = new List<BaseInteraction>();

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

        public override CharacterSetup GetCharacterSetup() => setup;

        public override void FindTargetToAttack(bool state)
        {
            hpViewController.ShowHideHp(!state || (state && CurrentHp < setup.maximumHp));
            var enemy = SessionManager.GetEnemy();

            if (Attachment.Target == null && !state)
                Attachment.Target = enemy;
            else
                Attachment.Target = null;
        }

        protected override void Update()
        {
            base.Update();
            foreach (var interaction in _interactions)
                interaction.Interaction(this);
        }

        public override void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out BaseInteraction interaction))
                if (!_interactions.Contains(interaction))
                {
                    interaction.OnEnter();
                    _interactions.Add(interaction);
                }
        }

        public override void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out BaseInteraction interaction))
                if (_interactions.Contains(interaction))
                {
                    interaction.OnExit();
                    _interactions.Remove(interaction);
                }
        }

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
            SessionManager.SetPlayer(this);
            WorkerStateMachine.Start(this);

            CurrentWeapon = new GunWeapon(weaponSetup, attackPosition);
            _container.Inject(CurrentWeapon);
            Initialize();
        }

        private void FixedUpdate()
        {
            Movable.Move(this);
        }
    }
}