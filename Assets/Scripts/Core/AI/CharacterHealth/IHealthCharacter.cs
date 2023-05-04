using System;

namespace Core.AI.CharacterHealth
{
    public interface IHealthCharacter
    {
        public event Action OnDestroyCharacter;
        public event Action<float> OnChangeHp;
        public float CurrentHp { get; }
        public void ApplyDamage(float damage);
        public void Health(float health);
    }
}