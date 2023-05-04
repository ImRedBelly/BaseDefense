using Core.AI;
using Core.AI.Workers;
using Interaction;
using Setups.Interactions;

namespace Core.Interaction
{
    public class TrashInteraction : BaseInteraction<TrashInteractionSetup>
    {
        public override bool CanInteract(Worker worker)
        {
            var isCan = worker.Inventory.Items.Count > 0;
            if (!isCan) return false;
           
            return base.CanInteract(worker);
        }

        public override void CompleteInteraction(Worker worker)
        {
            
            worker.Inventory.Clear();
            base.CompleteInteraction(worker);
        }
    }
}