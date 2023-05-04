using Core.ResourceEntity;
using UI;
using UnityEngine;

namespace Setups
{
    [CreateAssetMenu(fileName = "PrefabContainer", menuName = "PrefabContainer")]
    public class PrefabContainer : ScriptableObject
    {
        public RecipeCreatorController recipeCreatorController;
        public UIInteractionController uiInteractionController;
    }
}