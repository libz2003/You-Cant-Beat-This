using System;
using UnityEngine;

namespace EnemyAndPath
{
    [RequireComponent(typeof(PathWalker))]
    public class Enemy : MonoBehaviour
    {
        public PathWalker Walker { get; private set; }
        public EnemyHealth Health;

        public event Action<Enemy> OnEnemyDied;

        private Spawner ownerSpawner;

        public int Index { get; private set; }

        private void Awake()
        {
            Walker = GetComponent<PathWalker>();
            // children get health
            Health = transform.Find("hitbox").GetComponent<EnemyHealth>();
        }
        private void OnEnable()
        {
            if (Health != null)
            {
                Health.OnDied += HandleDied;
                gameObject.GetComponent<Animator>().enabled = true;
                gameObject.GetComponent<PathWalker>().enabled = true;
                gameObject.GetComponent<CharacterController>().enabled = true;
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
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
            SoundEffectManager.PlaySkeleDeath();
            gameObject.GetComponent<Animator>().enabled = false;
            gameObject.GetComponent<PathWalker>().enabled = false;
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.GetComponent<Rigidbody>().isKinematic = false;
            // add force at head to the right so it falls
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.AddForceAtPosition(transform.right * 10f, transform.position + new Vector3(0, 100, 0), ForceMode.Impulse);
            Destroy(gameObject, 1.0f);
        }

    }
}
