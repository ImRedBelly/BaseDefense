using Core.Interactions;
using UnityEngine;

namespace Setups.CharacterSetups
{
    [CreateAssetMenu(fileName = "BaseEnemySetup", menuName = "Setups/Characters/BaseEnemySetup", order = 0)]
    public class BaseEnemySetup : CharacterSetup
    {
        public MoneyInteraction rewardObject;
        public float chanceSpawnReward;
    }
}