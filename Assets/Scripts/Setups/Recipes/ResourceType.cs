using UnityEngine;

namespace Setups.Recipes
{
    [CreateAssetMenu(fileName = "ResourceType", menuName = "Setups/Resources/ResourceType", order = 0)]
    public class ResourceType : ScriptableObject
    {
        [field: SerializeField]public double ResourcePrice { get; private set; }
    }
}