using Core.AI.Characters;
using Library;
using Setups;
using Zenject;

namespace Core.AI.CharacterAttachment
{
    public class PlayerAttachment : Attachment
    {
        private PlayerController _character;
        [Inject] private PrefabContainer _prefabContainer;

        public PlayerAttachment(Character character)
        {
            _character = (PlayerController) character;
        }

        public override void Attack()
        {
            if (Target == null)
                _character.FindTargetToAttack(false);
            _character.Animator.SetTrigger(AnimationsPrefsNames.Attack);
            _character.CurrentWeapon.Attack();
        }
    }
}