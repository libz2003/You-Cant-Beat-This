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
        private bool touchTower = false;

        private void OnEnable()
        {
            // When re-used from a pool, reset state.
            isDead = false;
            touchTower = false;
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

            if (currentHealth == 0 && !isDead)
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("GunBullet"))
            {
                if (touchTower)
                    TakeDamage(10);
                else
                    TakeDamage(1);
            }
            if (other.gameObject.CompareTag("Bullet"))
            {
                if (touchTower)
                    TakeDamage(30);
                else
                    TakeDamage(2);
            }
            if (other.gameObject.CompareTag("GearBullet"))
            {
                TakeDamage(100);
            }
            if (other.gameObject.CompareTag("SmallExplode"))
            {
                TakeDamage(2);
            }
            if (other.gameObject.CompareTag("BigExplode"))
            {
                TakeDamage(4);
            }
            if (other.gameObject.CompareTag("Tower"))
            {
                touchTower = true;
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
