using System;
using System.Collections.Generic;
using System.Linq;
using Core.AI.Characters;
using Lean.Pool;
using Library.Data;
using UnityEngine;

namespace Core
{
    public class SessionManager
    {
        public CurrencyDataModel CurrencyDataModel { get; private set; } = new CurrencyDataModel();

        public event Action MainEnemyDestroy;
        public Action<bool> ChangeZonePlayer;

        public Character Player { get; private set; }
        public Character Enemy { get; private set; }
        private List<EnemyController> _enemies = new List<EnemyController>();

        public void SetPlayer(Character player) => Player = player;

        public void FindMainEnemy()
        {
            var enemy = _enemies.OrderBy(x =>
                x.GetDistanceToPlayer(Player.transform.position)).FirstOrDefault();
            Enemy = enemy;
        }

        public void AddEnemy(EnemyController enemyController)
        {
            if (!_enemies.Contains(enemyController))
                _enemies.Add(enemyController);
        }

        public void RemoveEnemy(EnemyController enemyController)
        {
            if (_enemies.Contains(enemyController))
            {
                if (enemyController == Enemy)
                {
                    _enemies.Remove(enemyController);
                    FindMainEnemy();
                    MainEnemyDestroy?.Invoke();
                }
            }
        }

        public void RestartGame()
        {
            _enemies.Clear();
        }
    }
}