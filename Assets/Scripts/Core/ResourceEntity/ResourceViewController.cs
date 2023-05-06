using System.Collections.Generic;
using System.Linq;
using Setups.Recipes;
using UnityEngine;

namespace Core.ResourceEntity
{
    public class ResourceViewController : MonoBehaviour
    {
        [SerializeField] private List<PointResource> ingredientPoints;


        public List<ResourceType> ResourceTypes
        {
            get { return ingredientPoints.Select(x => x.ResourceType).ToList(); }
        }

        public void Initialize(Dictionary<ResourceType, int> ingredients)
        {
            foreach (var point in ingredientPoints)
                if (ingredients.ContainsKey(point.ResourceType))
                {
                    point.gameObject.SetActive(true);
                    point.ActivateResourceView(ingredients[point.ResourceType], true);
                }
        }

        public void Reset()
        {
            foreach (var point in ingredientPoints)
            {
                point.ActivateResourceView(0, false);   
                point.gameObject.SetActive(false);
            }
        }
    }
}