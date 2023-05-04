using Core.AI;
using Core.AI.Workers;
using Lean.Pool;
using Setups;
using Setups.Interactions;
using Sirenix.OdinInspector;
using Storage;
using UI;
using UnityEngine;
using Zenject;

namespace Core.Interaction
{
    public abstract class BaseInteraction : SerializedMonoBehaviour
    {
        protected Inventory Inventory { get; } = new Inventory();
        
        [Inject] protected SignalBus SignalBus { get; }
        [Inject] protected PrefabContainer PrefabContainer;


        public abstract bool CanInteract(Worker worker);
        public abstract void Interact(Worker worker);

        public abstract void CompleteInteraction(Worker worker);

        public virtual void ProcessInteraction(float progress)
        {
        }

        public virtual void BreakUpInteraction(Worker worker)
        {
        }

        public virtual void ResetInteraction()
        {
        }

        public virtual void OnDetectEnter(Worker worker)
        {
        }

        public virtual void OnDetectExit(Worker worker)
        {
        }

        public virtual BaseInteractionSetup GetBaseInteractionSetup() => null;
    }

    public abstract class BaseInteraction<TSetup> : BaseInteraction where TSetup : BaseInteractionSetup
    {
        [SerializeField] protected TSetup setup;


        private UIInteractionController _uiInteractionControllerCopy;

        public override BaseInteractionSetup GetBaseInteractionSetup() => setup;

        protected virtual void OnEnable()
        {
        }

        protected virtual void OnDisable()
        {
        }

        private void OnMiniGameFinished(Worker obj)
        {
            CompleteInteraction(obj);
        }

        private void OnMiniGameExit(Worker obj)
        {
            BreakUpInteraction(obj);
        }

        public override bool CanInteract(Worker worker)
        {
            return true;
        }

        public override void Interact(Worker worker)
        {
            CreateUIInteractionController();
        }


        public override void ProcessInteraction(float progress)
        {
            if (_uiInteractionControllerCopy)
                _uiInteractionControllerCopy.SetProgress(progress);
        }

        public override void CompleteInteraction(Worker worker)
        {
            if (_uiInteractionControllerCopy)
                _uiInteractionControllerCopy.OnCompletedSlider();
        }

        public override void BreakUpInteraction(Worker worker)
        {
            if (_uiInteractionControllerCopy) _uiInteractionControllerCopy.OnCompletedSlider();
        }


        protected void CreateUIInteractionController()
        {
            if (setup.interactionTime == 0) return;
            var positionSpawnUIInteraction = transform.position + new Vector3(0, 2f, 0);

            _uiInteractionControllerCopy ??= LeanPool.Spawn(PrefabContainer.uiInteractionController,
                positionSpawnUIInteraction, Quaternion.identity, transform);

            _uiInteractionControllerCopy.Show();
        }
    }
}