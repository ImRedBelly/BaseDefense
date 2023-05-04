using Storage;
using System.Linq;
using Core.AI;
using Core.AI.Workers;
using Core.Interaction;
using Core.Storage;
using Core.Utils;
using UnityEngine;
using Setups.Interactions;

namespace Interaction
{
    public class TableInteraction : BaseInteraction<TableInteractionSetup>
    {
        [SerializeField] private InventoryView inventoryView;
        private Inventory TableInventory { get; } = new Inventory();
        private bool _isInteraction = true;
        private bool _isCompleteRecipe = false;

       

        protected override void OnEnable()
        {
            base.OnEnable();
            Inventory.OnChangeResource += inventoryView.UpdateView;
        }

      

        public override bool CanInteract(Worker worker)
        {
            bool canInteract = false;

            var isContains = false;
            foreach (var kv in worker.Inventory.Items)
            {
                foreach (var resourceType in setup.notAllowedResource)
                    if (!isContains && resourceType == kv.Key)
                    {
                        isContains = true;
                        break;
                    }
            }
            
            if (isContains) return false;
            

           
                TableInventory.Clear();
                TableInventory.Add(Inventory.Items.ToDictionary(x => x.Key,y => 1));
                TableInventory.Add(worker.Inventory.Items);

             
                

            return base.CanInteract(worker);
        }


        public override void CompleteInteraction(Worker worker)
        {
            if (!_isInteraction) return;


                if (Inventory.Items.Count > 0)
                {
                    TransitResource(worker);
                }
                else if (worker.Inventory.Items.Count > 0 && Inventory.Items.Count == 0)
                {
                    Inventory.Add(worker.Inventory.Items);
                    worker.Inventory.Clear();
                }

                _isInteraction = false;
                
                base.CompleteInteraction(worker);
            
            
        }


        private void TransitResource(Worker worker)
        {
            if (_isCompleteRecipe)
            {
                var resource = TableInventory.Items.Where(kv => !worker.Inventory.Items.ContainsKey(kv.Key))
                    .ToDictionary(x => x.Key, y => 1);
                worker.Inventory.Add(resource);

                worker.CompleteInteraction();
                foreach (var kv in resource)
                {
                    Inventory.Remove(kv.Key);
                }
            }
            else
            {
                worker.Inventory.Add(Inventory.Items.First().Key);
                Inventory.Remove(Inventory.Items.First().Key);
            }
        }


        public override void ResetInteraction() => _isInteraction = true;
        public override void OnDetectEnter(Worker worker) => _isInteraction = true;
    }
}