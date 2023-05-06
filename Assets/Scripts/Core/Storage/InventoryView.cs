using System.Collections.Generic;
using System.Linq;
using Core.ResourceEntity;
using Lean.Pool;
using Setups;
using Setups.Recipes;
using UnityEngine;
using Zenject;

namespace Core.Storage
{
    public class InventoryView : MonoBehaviour
    {
        [Inject] private PrefabContainer _prefabContainer;

        private ResourceCreatorController resourceCreator;
        
        public void UpdateView(Dictionary<ResourceType, int> ingredients)
        {
            Reset();

            if (ingredients.Where(x => x.Value > 0).ToList().Count <= 0) return;

            resourceCreator = LeanPool.Spawn(
                _prefabContainer.resourceCreatorController,
                transform.position, Quaternion.identity, transform);

            resourceCreator.transform.localPosition = Vector3.zero;
            resourceCreator.Initialize(ingredients);
        }

        public void Reset()
        {
            if (resourceCreator != null)
            {
                resourceCreator.ResetCreator();
                LeanPool.Despawn(resourceCreator);
                resourceCreator = null;
            }
        }

        public void SetViewRecipe(bool isView) =>
            resourceCreator.transform.localScale = isView ? Vector3.one : Vector3.zero;
    }
}