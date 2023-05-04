using System;
using Core.AI.Characters;
using Lean.Pool;
using UnityEngine;

namespace Core.Weapons
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private Rigidbody rbBullet;
        [SerializeField] private float speed;
        private float _damage;
        private Vector3 _currentDirection;

        public void Init(float damage, Vector3 direction)
        {
            _damage = damage;
            _currentDirection = direction;
        }

        private void FixedUpdate()
        {
            var moveDirection = _currentDirection * speed;
            rbBullet.velocity = moveDirection;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyController controller))
            {
                controller.ApplyDamage(_damage);
                LeanPool.Despawn(this);
            }
        }

        private void OnBecameVisible()
        {
            LeanPool.Despawn(this);
        }
    }
}