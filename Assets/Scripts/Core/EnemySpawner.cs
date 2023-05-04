using System.Collections;
using Core.AI.Characters;
using Lean.Pool;
using UnityEngine;
using Zenject;

namespace Core
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private Transform defaultTarget;
        [SerializeField] private EnemyController enemyController;
        private WaitForSeconds _seconds = new WaitForSeconds(4);

        private void Start()
        {
            StartCoroutine(SpawnEnemies());
        }

        private IEnumerator SpawnEnemies()
        {
            while (true)
            {
                var enemy = LeanPool.Spawn(enemyController, transform.position, Quaternion.identity);
                enemy.Attachment.DefaultTarget = defaultTarget;
                enemy.Initialize();
                yield return _seconds;
            }
        }
    }
}