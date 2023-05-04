using System.Collections.Generic;
using Setups.Recipes;
using UnityEngine;

namespace Setups.Interactions
{ 
    [CreateAssetMenu(fileName = "Table Setup", menuName = "Setups/Interactions/Table")]
    public class TableInteractionSetup : BaseInteractionSetup
    {
        public List<ResourceType> notAllowedResource;
    }
}
