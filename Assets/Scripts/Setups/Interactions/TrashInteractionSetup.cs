using System.Collections.Generic;
using Setups.Recipes;
using UnityEngine;

namespace Setups.Interactions
{
    [CreateAssetMenu(fileName = "Trash Setup", menuName = "Setups/Interactions/Trash")]
    public class TrashInteractionSetup : BaseInteractionSetup
    {
        public List<ResourceType> notAllowedResource;
    }
}