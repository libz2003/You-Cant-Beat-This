using System;
using UnityEngine;

namespace EnemyAndPath
{
    public class BankHealth : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private int maxHealth = 10;
        private int currentHealth;
        [Header("Rewards")]
        [SerializeField] private int rewardAmount = 20000;
        public int MaxHealth => maxHealth;
        public int CurrentHealth => currentHealth;
        public event Action<BankHealth> OnDied;
        private bool isDead;
        private void Start()
        {
            ResetHealth();
        }

        public void TakeDamage(int damage)
        {
            if (isDead)
                return;
            if (damage <= 0)
                return;
            currentHealth -= damage;
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                isDead = true;
                Die();
            }
        }
        public void ResetHealth()
        {
            if (PersistentSettings.instance.bankBreakable)
            {
                currentHealth = maxHealth;
            }
            else // bank not breakable
            {
                currentHealth = 1000000;
            }
            isDead=false;
        }
        private void Die()
        {
            SoundEffectManager.PlayBankExplosion();
            PersistentSettings.instance.targetBankBreakable = false;
            OnDied?.Invoke(this);
            PlayerStats.Money += rewardAmount;
            // Destroy(gameObject);
            transform.Find("Smoke").gameObject.SetActive(true);
            transform.Find("Nuke").gameObject.SetActive(true);
            transform.Find("Fire").gameObject.SetActive(true);
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                TakeDamage(10);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("GunBullet"))
            {
                TakeDamage(4);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("GearBullet"))
            {
                TakeDamage(20);
                Destroy(collision.gameObject);
            }
            else if (collision.gameObject.CompareTag("Enemy"))
            {
                PlayerStats.Lives--;
                if (PlayerStats.Lives == 0) Universe.instance.GameOver();
                Destroy(collision.gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                PlayerStats.Lives--;
                if (PlayerStats.Lives == 0) Universe.instance.GameOver();
                Destroy(other.gameObject);
            }
        }
    }
}
