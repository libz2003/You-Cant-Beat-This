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
        private int deathCount = 0;
        private bool deathSoundPlayed = false;

        private void Start()
        {
            if (PersistentSettings.instance.playThroughCount == 0)
            {
                _spawnTimer = 17.5f;
            }else 
            {
                _spawnTimer = 0.0f;
            }
            Enemy.OnEnemyDied += HandleEnemyDied;
        }

        private void OnDestroy()
        {
            Enemy.OnEnemyDied -= HandleEnemyDied;
        }

        private void HandleEnemyDied(Enemy _)
        {
            deathCount++;
            if(deathCount <= 4 && !PersistentSettings.instance.foundBug && !deathSoundPlayed) {
                if (AudioManager.Instance.tryPlayClip(AudioManager.Instance.killEnemy))
                {
                    deathSoundPlayed = true;
                }
            }
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
