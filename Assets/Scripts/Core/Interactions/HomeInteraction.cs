using System.Linq;
using UnityEngine;
using Core.AI.Characters;

namespace Core.Interactions
{
    public class HomeInteraction : BaseInteraction
    {
        private float _timeInteract;
        private float _timeToHeath = 0.5f;

        public override void Interaction(Character character)
        {
            base.Interaction(character);
            _timeInteract += Time.deltaTime;
            if (_timeInteract > _timeToHeath)
            {
                _timeInteract = 0;
                character.Health(1);
            }
        }

        public override void OnEnter(Character character)
        {
            base.OnEnter(character);
            SessionManager.ChangeZonePlayer.Invoke(true);
            var allMoney = character.Inventory.Items.Sum(x => x.Value);
            character.Inventory.Clear();
            SessionManager.CurrencyDataModel.AppendCurrency(allMoney);
        }

        public override void OnExit()
        {
            base.OnExit();
            SessionManager.ChangeZonePlayer.Invoke(false);
        }
    }
}