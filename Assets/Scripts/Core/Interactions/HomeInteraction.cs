using Core.AI.Characters;
using UnityEngine;

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

        public override void OnEnter()
        {
            base.OnEnter();
            SessionManager.ChangeZonePlayer.Invoke(true);
        }

        public override void OnExit()
        {
            base.OnExit();
            SessionManager.ChangeZonePlayer.Invoke(false);
        }
    }
}