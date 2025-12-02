using System;
using UnityEngine;
using UnityEngine.UI;

namespace EnemyAndPath
{
    public class EnemyHealth : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private int maxHealth = 100;
        private int currentHealth;

        [Header("Optional UI")]
        [SerializeField] private Slider healthSlider;

        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;

        // Fired whenever health changes (including reset).
        public event Action<EnemyHealth> OnHealthChanged;

        // Fired once when health reaches zero.
        public event Action<EnemyHealth> OnDied;

        private bool isDead;

        private void OnEnable()
        {
            // When re-used from a pool, reset state.
            isDead = false;
            ResetHealth();
        }

        /// <summary>
        /// Apply damage to this enemy.
        /// </summary>
        public void TakeDamage(int damage)
        {
            if (isDead) return;
            if (damage <= 0) return;

            currentHealth -= damage;
            if (currentHealth < 0)
            {
                currentHealth = 0;
            }

            UpdateSlider();
            OnHealthChanged?.Invoke(this);

            if (currentHealth == 0)
            {
                isDead = true;
                OnDied?.Invoke(this);
            }
        }

        /// <summary>
        /// Reset health back to max.
        /// </summary>
        public void ResetHealth()
        {
            currentHealth = maxHealth;
            isDead = false;
            UpdateSlider();
            OnHealthChanged?.Invoke(this);
        }

        /// <summary>
        /// Optional: basic collision-based damage.
        /// </summary>
        private void OnCollisionEnter(Collision collision)
        {
            // Keep this for compatibility with your current towers.
            if (collision.gameObject.CompareTag("GunBullet"))
            {
                TakeDamage(1);
            }
            if (collision.gameObject.CompareTag("Bullet"))
            {
                TakeDamage(20);
            }
        }

        private void UpdateSlider()
        {
            if (healthSlider == null || maxHealth <= 0) return;

            float normalized = (float)currentHealth / (float)maxHealth;
            healthSlider.value = normalized;
        }
    }
}
