using Library;
using Core.AI.Characters;

namespace Core.AI.CharacterAttachment
{
    public class PlayerAttachment : Attachment
    {
        private PlayerController _character;

        public PlayerAttachment(Character character)
        {
            _character = (PlayerController) character;
        }

        public override void Attack()
        {
            if (Target != null)
            {
                _character.Animator.SetTrigger(AnimationsPrefsNames.Attack);
                _character.CurrentWeapon.Attack();
            }
            else
                _character.FindTargetToAttack(false);
        }
    }
}