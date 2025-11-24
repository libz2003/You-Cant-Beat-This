using UnityEngine;

namespace EnemyAndPath
{
    public class Spawner : MonoBehaviour
    {
        public Transform[] waypoints;
        public int remainingSpawnNumber = 10;

        [SerializeField] private float spawnInterval = 1f;

        private ObjectPooler pool;
        private float _spawnTimer;

        // Wave tracking
        private int spawnedCount;
        private int finishedCount;
        private bool waveCompleted;

        private int nextEnemyIndex = 0;

        private void Start()
        {
            pool = GetComponent<ObjectPooler>();
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
            GameObject spawnedObject = pool.GetPooledObject();
            if (spawnedObject == null)
            {
                Debug.LogWarning("Spawner: ObjectPooler returned null pooled object.");
                return;
            }

            Enemy enemy = spawnedObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                // Initialize with waypoints and register this spawner.
                enemy.Initialize(waypoints, this, nextEnemyIndex++);
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
