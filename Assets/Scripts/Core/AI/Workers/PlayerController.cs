using System;
using System.Collections.Generic;
using System.Linq;
using Core.AI.WorkerAttachment;
using UnityEngine;
using Core.AI.WorkersMovable;
using Core.Interactions;
using Setups.CharacterSetups;

namespace Core.AI.Workers
{
    public sealed class PlayerController : Character<PlayerSetup>
    {
        [SerializeField] private Transform orientation;

        private void Awake()
        {
            Movable = new RigidbodyMovable(this, GetComponent<Rigidbody>());
            Attachment = new PlayerAttacment(this);
            WorkerStateMachine.Start(this);
            SessionManager.SetPlayer(this);
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

        protected override void ChangeState(bool state)
        {
            base.ChangeState(state);
            if (!state)
                FindTargetToAttack();
        }

        public override CharacterSetup GetCharacterSetup() => setup;

        public override void FindTargetToAttack()
        {
            var enemy = SessionManager.GetEnemy();
            if (Attachment.Target == null)
                Attachment.Target = enemy;
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