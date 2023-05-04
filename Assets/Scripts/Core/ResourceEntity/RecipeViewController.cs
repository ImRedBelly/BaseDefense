using System.Collections.Generic;
using System.Linq;
using Setups.Recipes;
using UnityEngine;

namespace Core.ResourceEntity
{
    public class RecipeViewController : MonoBehaviour
    {
        [SerializeField] private List<PointIngredient> ingredientPoints;


        public List<ResourceType> ResourceTypes
        {
            get { return ingredientPoints.Select(x => x.ResourceType).ToList(); }
        }

        public void Initialize(Dictionary<ResourceType, int> ingredients)
        {
            foreach (var point in ingredientPoints)
                if (ingredients.ContainsKey(point.ResourceType))
                    point.gameObject.SetActive(true);
        }

        public void Reset()
        {
            foreach (var point in ingredientPoints)
                point.gameObject.SetActive(false);
        }
    }
}