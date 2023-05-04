using System;
using Core.AI.CharacterHealth;
using Core.AI.CharacterMovable;
using Core.AI.WorkerAttachment;
using Core.StateMachine;
using Core.StateMachine.States;
using Setups;
using Setups.CharacterSetups;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Core.AI.Workers
{
    public abstract class Character : SerializedMonoBehaviour, IHealthCharacter
    {
        public event Action<float> OnChangeHp;
        public event Action OnDestroyCharacter;
        public float CurrentHp { get; private set; }
        public abstract Vector3 Direction { get; }
        public Attachment Attachment { get; protected set; }
        public Animator Animator { get; protected set; }

        public WorkerStateMachine WorkerStateMachine = new WorkerStateMachine();

        protected Movable Movable { get; set; }
        [Inject] protected SessionManager SessionManager;

        protected virtual void Start()
        {
            CurrentHp = GetCharacterSetup().maximumHp;
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

            FindTargetToAttack(state);
        }

        public abstract CharacterSetup GetCharacterSetup();
        public abstract void FindTargetToAttack(bool state);

        public virtual void OnTriggerEnter(Collider other)
        {
        }

        public virtual void OnTriggerExit(Collider other)
        {
        }


        public void ApplyDamage(float damage)
        {
            CurrentHp -= damage;
            OnChangeHp?.Invoke(CurrentHp / GetCharacterSetup().maximumHp);
            if (CurrentHp <= 0)
            {
                OnDestroyCharacter?.Invoke();
             Debug.LogError("DEAD: " + gameObject.name);   
            }
        }

        public void Health(float health)
        {
            CurrentHp += health;
            OnChangeHp?.Invoke(CurrentHp / GetCharacterSetup().maximumHp);
        }
    }

    public abstract class Character<TSetup> : Character where TSetup : CharacterSetup
    {
        [SerializeField] protected TSetup setup;
        [SerializeField] protected WeaponSetup weaponSetup;

        protected override void Start()
        {
            base.Start();
            Animator = GetComponentInChildren<Animator>();
            WorkerStateMachine.Start(this);
        }

        protected virtual void Update()
        {
            WorkerStateMachine.Update();
        }
    }
}