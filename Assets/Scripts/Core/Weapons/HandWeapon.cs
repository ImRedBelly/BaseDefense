using Core.AI.Characters;
using Library;
using Setups;

namespace Core.Weapons
{
    public class HandWeapon : IWeapon
    {
        private Character _character;
        private WeaponSetup _weaponSetup;
        public HandWeapon(Character character, WeaponSetup weaponSetup)
        {
            _character = character;
            _weaponSetup = weaponSetup;
        }
        public void Attack()
        {
            _character.Attachment.Target.ApplyDamage(_weaponSetup.damage);
            _character.Animator.SetTrigger(AnimationsPrefsNames.Attack);
        }
    }
}