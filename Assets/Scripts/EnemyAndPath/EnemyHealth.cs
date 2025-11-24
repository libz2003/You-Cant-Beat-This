
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EnemyAndPath
{
    public class EnemyHealth : MonoBehaviour
    {
        public Slider healthSlider;
        public int maxHealth = 10;
        private int health = 10;

        // function for getting shot
        public void TakeDamage(int damage)
        {
            health -= damage;
            healthSlider.value = health / (float)maxHealth;

            if (health <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        // detect collision with Tower and take damage
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Tower"))
            {
                TakeDamage(1);
            }
        }

        public void ResetHealth()
        {
            health = maxHealth;
            healthSlider.value = 1f;
        }
    }
}
