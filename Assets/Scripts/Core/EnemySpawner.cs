using System;
using Zenject;
using Lean.Pool;
using UnityEngine;
using System.Collections;
using System.Linq;
using Core.AI.Characters;
using Sirenix.Utilities;

namespace Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform defaultTarget;
        [SerializeField] private EnemyController enemyController;

        [Inject] private SessionManager _sessionManager;
        private WaitForSeconds _timeActiveSpawn = new WaitForSeconds(4);
        private WaitForSeconds _timePassiveSpawn = new WaitForSeconds(10);

        private Coroutine _coroutine;

        private void OnEnable() => _sessionManager.ChangeZonePlayer += ChangeSpawnEnemy;
        private void OnDisable() => _sessionManager.ChangeZonePlayer -= ChangeSpawnEnemy;

        private void ChangeSpawnEnemy(bool state)
        {
            if (_coroutine != null) StopCoroutine(_coroutine);
            _coroutine = StartCoroutine(SpawnEnemies(state));
        }

        private IEnumerator SpawnEnemies(bool state)
        {
            while (true)
            {
                var enemy = LeanPool.Spawn(enemyController, transform.position, Quaternion.identity);
                enemy.Attachment.DefaultTarget = defaultTarget;
                enemy.FindTargetToAttack(state);
                enemy.Initialize();
                yield return state ? _timePassiveSpawn : _timeActiveSpawn;
            }
        }
    }
}