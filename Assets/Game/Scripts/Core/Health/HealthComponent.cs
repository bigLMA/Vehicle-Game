using System;
using UnityEngine;
using VehicleGame.Core.Interfaces;

namespace VehicleGame.Core.Health
{
    public class HealthComponent : MonoBehaviour, IHealth
    {
        public int current { get; private set; }

        public int max { get; private set; }

        public event Action<int, int> OnHealthChange;
        public event Action OnDeath;

        public void Setup(int maxHealth)
        {
            max = maxHealth;
            current = max;
        }

        public void ResetHealth()
        {
            current = max;
            OnHealthChange?.Invoke(current, max);
        }

        public void TakeDamage(int damage)
        {
            current = Mathf.Clamp(current - damage, 0, max);

            if (current == 0)
            {
                OnDeath?.Invoke();
            }
            else
            {
                OnHealthChange?.Invoke(current, max);
            }
        }
    }

}

