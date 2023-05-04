using System.Collections.Generic;
using System.Linq;
using Core.AI.WorkersMovable;
using Core.Interaction;
using Core.Storage;
using Core.Utils;
using Library;
using Setups;
using Setups.Recipes;
using Sirenix.OdinInspector;
using StateMachine;
using Storage;
using UnityEngine;
using Zenject;

namespace Core.AI.Workers
{
    public abstract class Worker : SerializedMonoBehaviour
    {
        public abstract Vector3 Direction { get; }
        public Movable Movable { get; protected set; } 
        public Inventory Inventory { get; } = new Inventory();

        public WorkerStateMachine WorkerStateMachine = new WorkerStateMachine();


        [SerializeField] private InventoryView inventoryView;
        private readonly List<BaseInteraction> _possibleInteractions = new List<BaseInteraction>();

        protected virtual void Start()
        {
            WorkerStateMachine.Start(this);
        }

        private void OnEnable()
        {
            Inventory.OnChangeResource += ChangeInventory;

        }

    
        protected virtual void OnDisable()
        {
            Inventory.OnChangeResource -= ChangeInventory;
        }
        
        protected virtual void ChangeInventory(Dictionary<ResourceType, int> ingredients)
        {
            Movable.Animator.SetBool(AnimationsPrefsNames.IsCarrying, ingredients.Count > 0);
            inventoryView.UpdateView(ingredients);
        }

        protected virtual void Update()
        {
            WorkerStateMachine.Update();
        }

        private void OnTriggerEnter(Collider other)
        {
            var interaction = other.GetComponentInParent<BaseInteraction>();
            if (interaction != null && !_possibleInteractions.Contains(interaction))
            {
                _possibleInteractions.Add(interaction);
                interaction.OnDetectEnter(this);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var interaction = other.GetComponentInParent<BaseInteraction>();
            if (interaction != null && _possibleInteractions.Contains(interaction))
            {
                _possibleInteractions.Remove(interaction);
                interaction.OnDetectExit(this);
            }
        }

        public void CompleteInteraction()
        {
           
        }

        public BaseInteraction GetFirstBaseInteraction()
        {
            var firstBaseInteraction = _possibleInteractions.FirstOrDefault(x => x.GetBaseInteractionSetup());
            return firstBaseInteraction;
        }

        public BaseInteraction GetFirstAllowedBaseInteraction()
        {
            BaseInteraction toInteract = null;
            var minAngle = 180f; // 60 standart angle interaction

            foreach (var interaction in _possibleInteractions.ToList())
            {
                if (interaction.gameObject.activeInHierarchy == false)
                {
                    interaction.OnDetectExit(this);
                    _possibleInteractions.Remove(interaction);
                    continue;
                }

                var player = new Vector3(transform.position.x, 0, transform.position.z);
                var interact = new Vector3(interaction.transform.position.x, 0, interaction.transform.position.z);
                var angle = AngleBetweenVector2(player, interact);

                float AngleBetweenVector2(Vector3 vec1, Vector3 vec2)
                {
                    Vector3 diference = vec2 - vec1;
                    return Vector3.Angle(transform.forward, diference);
                }

                if (angle < minAngle && angle < interaction.GetBaseInteractionSetup().interactionAngle)
                {
                    minAngle = interaction.GetBaseInteractionSetup().interactionAngle;
                    toInteract = interaction;
                }
            }

            return toInteract;
        }
        
    }
}