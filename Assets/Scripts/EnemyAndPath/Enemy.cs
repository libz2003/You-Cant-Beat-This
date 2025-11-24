using System;
using UnityEngine;

namespace EnemyAndPath
{
    [RequireComponent(typeof(PathWalker))]
    [RequireComponent(typeof(EnemyHealth))]
    public class Enemy : MonoBehaviour
    {
        public PathWalker Walker { get; private set; }
        public EnemyHealth Health { get; private set; }

        public event Action<Enemy> OnEnemyDied;

        private Spawner ownerSpawner;

        public int Index { get; private set; }

        private void Awake()
        {
            Walker = GetComponent<PathWalker>();
            Health = GetComponent<EnemyHealth>();
        }

        private void OnEnable()
        {
            if (Health != null)
            {
                Health.OnDied += HandleDied;
            }
        }

        private void OnDisable()
        {
            if (Health != null)
            {
                Health.OnDied -= HandleDied;
            }

            // Notify spawner that this enemy is finished (either died or reached the end),
            // as long as we were initialized with a spawner.
            if (ownerSpawner != null)
            {
                ownerSpawner.NotifyEnemyFinished(this);
            }
        }

        /// <summary>
        /// Called by the spawner when this enemy is spawned or taken from the pool.
        /// </summary>
        public void Initialize(Transform[] waypoints, Spawner spawner, int index)
        {
            ownerSpawner = spawner;
            Index = index;
            if (Walker != null && waypoints != null && waypoints.Length > 0)
            {
                Walker.SetWaypoints(waypoints);
            }

            if (Health != null)
            {
                Health.ResetHealth();
            }
        }

        private void HandleDied(EnemyHealth _)
        {
            OnEnemyDied?.Invoke(this);

            // Disable the enemy. OnDisable will notify the spawner that we are finished.
            gameObject.SetActive(false);
        }
    }
}
