using Core.AI.Characters;
using Lean.Pool;
using Setups.Recipes;
using UnityEngine;

namespace Core.Interactions
{
    public class MoneyInteraction : BaseInteraction
    {
        [SerializeField] private ResourceType moneyResource;

        public override void Initialize()
        {
            base.Initialize();
            SessionManager.OnRestartGame += DestroyMoney;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            SessionManager.OnRestartGame -= DestroyMoney;
        }

        public override void OnEnter(Character character)
        {
            base.OnEnter(character);
            character.Inventory.Add(moneyResource);
            DestroyMoney();
        }

        private void DestroyMoney()
        {
            LeanPool.Despawn(this);
        }
    }
}