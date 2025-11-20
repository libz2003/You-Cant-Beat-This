using UnityEngine;

namespace EnemyAndPath
{
    public class Spawner : MonoBehaviour
    {
        public Transform[] waypoints;
        public int remainingSpawnNumber = 10;
        public string enemyTag = "Enemy";

        private float _spawnTimer;
        [SerializeField] private float spawnInterval = 1f;

        private ObjectPooler pool;

        private void Start()
        {
            pool = gameObject.GetComponent<ObjectPooler>();
        }

        void Update()
        {
            if (remainingSpawnNumber > 0)
            {
                _spawnTimer -= Time.deltaTime;
                if (_spawnTimer <= 0)
                {
                    _spawnTimer = spawnInterval;
                    SpawnEnemy();
                }
            }
            else
            {
                // we check if all enemies are dead
                GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
                if (enemies.Length == 0)
                {
                    Universe.instance.Win();
                }
            }
        }

        private void SpawnEnemy()
        {
            GameObject spawnedObject = pool.GetPooledObject();
            spawnedObject.transform.position = waypoints[0].position;
            spawnedObject.GetComponent<PathWalker>().waypoints = waypoints;
            spawnedObject.SetActive(true);
            remainingSpawnNumber--;
        }
    }
}
