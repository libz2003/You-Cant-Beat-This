using Map;
using UnityEngine;

namespace EnemyAndPath
{
    public class Spawner : MonoBehaviour
    {
        // public Transform[] waypoints; // now use GridWaypointContainer.Waypoints 
        public int remainingSpawnNumber = 15;
        public GameObject enemyPrefab;

        [SerializeField] private float spawnInterval = 1f;

        private float _spawnTimer;

        // Wave tracking
        private int spawnedCount;
        private int finishedCount;
        private bool waveCompleted;

        private int nextEnemyIndex = 0;

        private void Start()
        {
            _spawnTimer = spawnInterval;
        }

        private void Update()
        {
            // 1. Spawn enemies while we still have some to spawn.
            if (remainingSpawnNumber > 0)
            {
                _spawnTimer -= Time.deltaTime;
                if (_spawnTimer <= 0f)
                {
                    _spawnTimer = spawnInterval;
                    SpawnEnemy();
                }
            }
            // 2. When no more to spawn, wait until all spawned enemies are finished.
            else if (!waveCompleted)
            {
                // All spawned enemies have either died or reached the end.
                if (spawnedCount > 0 && finishedCount >= spawnedCount && PlayerStats.Lives > 0)
                {
                    waveCompleted = true;
                    Universe.instance.Win();
                }
            }
        }

        private void SpawnEnemy()
        {
            GameObject spawnedObject = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
            if (spawnedObject == null)
            {
                Debug.LogWarning("Spawner: ObjectPooler returned null pooled object.");
                return;
            }

            Enemy enemy = spawnedObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Initialize with waypoints and register this spawner.
                // enemy.Initialize(waypoints, this, nextEnemyIndex++);
                enemy.Initialize(GridWaypointContainer.Waypoints, this, nextEnemyIndex++);
            }


            spawnedObject.SetActive(true);
            remainingSpawnNumber--;
            spawnedCount++;
        }

        // Called by Enemy when it is finished (died or reached the end of the path).
        public void NotifyEnemyFinished(Enemy enemy)
        {
            finishedCount++;
        }
    }
}
