using System.Collections.Generic;
using Core.AI.CharacterAttachment;
using Core.AI.CharacterMovable;
using Core.Interactions;
using Core.Storage;
using Core.Weapons;
using Setups.CharacterSetups;
using UI;
using UnityEngine;

namespace Core.AI.Characters
{
    public sealed class PlayerController : Character<PlayerSetup>
    {
        public IWeapon CurrentWeapon;

        [SerializeField] private Transform orientation;
        [SerializeField] private Transform attackPosition;
        [SerializeField] private HpViewController hpViewController;
        [SerializeField] private InventoryView inventoryView;

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
            hpViewController.ShowHideHp(!state || (CurrentHp < setup.maximumHp));
            if (!state) FindMainEnemy();
            else Attachment.Target = null;
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
                    interaction.OnEnter(this);
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
            SessionManager.MainEnemyDestroy += FindMainEnemy;
            Inventory.OnChangeResource += inventoryView.UpdateView;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            OnChangeHp += hpViewController.SetProgress;
            SessionManager.MainEnemyDestroy -= FindMainEnemy;
            Inventory.OnChangeResource -= inventoryView.UpdateView;
        }

        private void Awake()
        {
            Movable = new RigidbodyMovable(this, GetComponent<Rigidbody>());
            Attachment = new PlayerAttachment(this);
            SessionManager.SetPlayer(this);
            WorkerStateMachine.Start(this);

            CurrentWeapon = new GunWeapon(weaponSetup, attackPosition);
            Container.Inject(CurrentWeapon);
            Initialize();
        }

        private void FixedUpdate()
        {
            Movable.Move(this);
        }

        private void FindMainEnemy()
        {
            SessionManager.FindMainEnemy();
            Attachment.Target = SessionManager.Enemy;
        }
    }
}