using UnityEngine;

namespace EnemyAndPath
{
    public class Spawner : MonoBehaviour
    {
        public Transform[] waypoints;
        
        private float _spawnTimer;
        [SerializeField] private float spawnInterval = 1f;

        private ObjectPooler pool;

        private void Start()
        {
            pool = gameObject.GetComponent<ObjectPooler>();
        }

        void Update()
        {
            _spawnTimer -= Time.deltaTime;
            if (_spawnTimer <= 0)
            {
                _spawnTimer = spawnInterval;
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            GameObject spawnedObject = pool.GetPooledObject();
            spawnedObject.transform.position = waypoints[0].position;
            spawnedObject.GetComponent<PathWalker>().waypoints = waypoints;
            spawnedObject.SetActive(true);
        }
    }
}
