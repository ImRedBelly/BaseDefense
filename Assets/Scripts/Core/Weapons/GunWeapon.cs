using Lean.Pool;
using Setups;
using UnityEngine;
using Zenject;

namespace Core.Weapons
{
    public class GunWeapon : IWeapon
    {
        [Inject] private PrefabContainer _prefabContainer;
        
        private WeaponSetup _weaponSetup;
        private Transform _attackPosition;

        public GunWeapon(WeaponSetup weaponSetup, Transform attackPosition)
        {
            _weaponSetup = weaponSetup;
            _attackPosition = attackPosition;
        }

        public void Attack()
        {
            var bullet = LeanPool.Spawn(_prefabContainer.bulletController, _attackPosition.position, Quaternion.identity);
            bullet.Init(_weaponSetup.damage, _attackPosition.forward);
        }
    }
}