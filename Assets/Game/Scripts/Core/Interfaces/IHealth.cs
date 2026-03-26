using System;

namespace VehicleGame.Core.Interfaces
{
    public interface IHealth : IDamageable
    {
        public int current { get; }
        public int max { get; }

        void Setup(int maxHealth);
        void ResetHealth();

        public event Action<int, int> OnHealthChange;
        public event Action OnDeath;
    }
}