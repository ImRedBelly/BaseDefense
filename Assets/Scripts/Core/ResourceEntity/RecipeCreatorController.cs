using System.Collections.Generic;
using DG.Tweening;
using Setups.Recipes;
using UnityEngine;

namespace Core.ResourceEntity
{
    public class RecipeCreatorController : MonoBehaviour
    {
        [SerializeField] private List<RecipeViewController> recipeViewControllers;

        public void Initialize(Dictionary<ResourceType, int> ingredients)
        {
            ResetCreator();

            foreach (var recipeView in recipeViewControllers)
            {
                bool theSame = true;
                foreach (var kv in ingredients)
                    if (theSame)
                        theSame = recipeView.ResourceTypes.Contains(kv.Key);
                if (theSame)
                {
                    recipeView.gameObject.SetActive(true);
                    recipeView.Initialize(ingredients);
                    break;
                }
            }
        }

        public void ResetCreator()
        {
            foreach (var recipeView in recipeViewControllers)
            {
                recipeView.Reset();
                recipeView.gameObject.SetActive(false);
            }
        }

        private void OnDisable()
        {
            DOTween.Kill(transform);
        }
    }
}