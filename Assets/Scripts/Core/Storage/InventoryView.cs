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

        private RecipeCreatorController _recipeCreator;
        
        public void UpdateView(Dictionary<ResourceType, int> ingredients)
        {
            Reset();

            if (ingredients.Where(x => x.Value > 0).ToList().Count <= 0) return;

            _recipeCreator = LeanPool.Spawn(
                _prefabContainer.recipeCreatorController,
                transform.position, Quaternion.identity, transform);

            _recipeCreator.transform.localPosition = Vector3.zero;
            _recipeCreator.Initialize(ingredients);
        }

        public void Reset()
        {
            if (_recipeCreator != null)
            {
                _recipeCreator.ResetCreator();
                LeanPool.Despawn(_recipeCreator);
                _recipeCreator = null;
            }
        }

        public void SetViewRecipe(bool isView) =>
            _recipeCreator.transform.localScale = isView ? Vector3.one : Vector3.zero;
    }
}