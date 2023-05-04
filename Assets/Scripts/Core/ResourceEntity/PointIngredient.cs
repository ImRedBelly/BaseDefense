using Setups.Recipes;
using UnityEngine;

namespace Core.ResourceEntity
{
    public class PointIngredient : MonoBehaviour
    {
        [SerializeField] private ResourceType resourceType;
        public ResourceType ResourceType => resourceType;
    }
}