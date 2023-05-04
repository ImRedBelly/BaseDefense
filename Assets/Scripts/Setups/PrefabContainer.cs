using Core.ResourceEntity;
using Core.Weapons;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

namespace Setups
{
    [CreateAssetMenu(fileName = "PrefabContainer", menuName = "PrefabContainer")]
    public class PrefabContainer : ScriptableObject
    {
        public BulletController bulletController;
        public RecipeCreatorController recipeCreatorController;
        public HpViewController hpViewController;
    }
}