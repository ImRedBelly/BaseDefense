using UnityEngine;

namespace Setups
{
    [CreateAssetMenu(fileName = "WeaponSetup", menuName = "Setups/Weapons/WeaponSetup", order = 0)]
    public class WeaponSetup : ScriptableObject
    {
        public float damage;
    }
}