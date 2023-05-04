using System;
using Core.AI.WorkerAttachment;
using Core.AI.WorkersMovable;
using Core.StateMachine;
using Core.StateMachine.States;
using Setups.CharacterSetups;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Core.AI.Workers
{
    public abstract class Character : SerializedMonoBehaviour
    {
        public abstract Vector3 Direction { get; }
        public Movable Movable { get; protected set; }
        public Attachment Attachment { get; protected set; }
        public Animator Animator { get; protected set; }

        public WorkerStateMachine WorkerStateMachine = new WorkerStateMachine();
        [Inject] protected SessionManager SessionManager;

        protected virtual void Start()
        {
        }

        protected virtual void OnEnable()
        {
            SessionManager.ChangeZonePlayer += ChangeState;
        }

        protected virtual void OnDisable()
        {
            SessionManager.ChangeZonePlayer -= ChangeState;
        }

        protected virtual void ChangeState(bool state)
        {
            if (state)
                WorkerStateMachine.ChangeState(new PeaceState());
            else
                WorkerStateMachine.ChangeState(new AttackState());
        }

        public abstract CharacterSetup GetCharacterSetup();
        public abstract void FindTargetToAttack();

        public virtual void OnTriggerEnter(Collider other)
        {
        }

        public virtual void OnTriggerExit(Collider other)
        {
        }
    }

    public abstract class Character<TSetup> : Character where TSetup : CharacterSetup
    {
        [SerializeField] protected TSetup setup;

        protected virtual void Start()
        {
            Animator = GetComponentInChildren<Animator>();
            WorkerStateMachine.Start(this);
        }

        protected virtual void Update()
        {
            WorkerStateMachine.Update();
        }
    }
}