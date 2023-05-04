using System;
using System.Collections.Generic;
using System.Linq;
using Core.AI.Workers;

namespace Core
{
    public class SessionManager
    {
        public Action<bool> ChangeZonePlayer;

        public Character Player { get; private set; }
        private List<EnemyController> _enemies = new List<EnemyController>();

        public void SetPlayer(Character player) => Player = player;
        public EnemyController GetEnemy()
        {
            return _enemies.FirstOrDefault();
        }
        public void AddEnemy(EnemyController enemyController)
        {
            if(!_enemies.Contains(enemyController))
                _enemies.Add(enemyController);
        }
        public void RemoveEnemy(EnemyController enemyController)
        {
            if(_enemies.Contains(enemyController))
                _enemies.Remove(enemyController);
        }

    }
}