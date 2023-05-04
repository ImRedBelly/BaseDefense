using Core.AI;
using Core.AI.Workers;
using Core.Utils;
using Setups.Interactions;

namespace Core.Interaction
{
    public class SpawnerInteraction : BaseInteraction<SpawnerInteractionSetup>
    {
        public override bool CanInteract(Worker worker)
        {
            
            return false;
        }

        public override void CompleteInteraction(Worker worker)
        {
            worker.Inventory.Add(setup.resource);
            base.CompleteInteraction(worker);
        }
    }
}