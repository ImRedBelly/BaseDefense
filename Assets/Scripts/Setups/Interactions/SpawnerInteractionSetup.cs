using UnityEngine;
using Setups.Recipes;

namespace Setups.Interactions
{
    [CreateAssetMenu(fileName = "Spawner", menuName = "Setups/Interactions/Spawner")]
    public class SpawnerInteractionSetup : BaseInteractionSetup
    {
        public ResourceType resource;
    }
}