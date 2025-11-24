using UnityEngine;
using UnityEngine.UI;

namespace EnemyAndPath
{
    [RequireComponent(typeof(Slider))]
    public class EnemyHealthUI : MonoBehaviour
    {
        [SerializeField] private EnemyHealth targetHealth;

        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        private void OnEnable()
        {
            if (targetHealth == null)
            {
                targetHealth = GetComponentInParent<EnemyHealth>();
            }

            if (targetHealth != null)
            {
                targetHealth.OnHealthChanged += HandleHealthChanged;
                HandleHealthChanged(targetHealth);
            }
        }

        private void OnDisable()
        {
            if (targetHealth != null)
            {
                targetHealth.OnHealthChanged -= HandleHealthChanged;
            }
        }

        private void HandleHealthChanged(EnemyHealth health)
        {
            if (health == null || slider == null || health.MaxHealth <= 0) return;

            float normalized = (float)health.CurrentHealth / (float)health.MaxHealth;
            slider.value = normalized;
        }
    }
}
