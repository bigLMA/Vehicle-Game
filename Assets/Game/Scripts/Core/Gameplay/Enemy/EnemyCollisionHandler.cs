using System;
using UnityEngine;
using VehicleGame.Core.Interfaces;

namespace VehicleGame.Core.Gameplay.Enemy
{
    public class EnemyCollisionHandler : MonoBehaviour
    {
        private int _damage;
        private IVehicle _target;

        public event Action OnCollisionWithVehicle;

        public void Configure(IVehicle vehicle, int damage)
        {
            _damage = damage;
            _target = vehicle;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<IVehicle>(out _))
            {
                _target.TakeDamage(_damage);

                OnCollisionWithVehicle?.Invoke();
            }
        }
    }
}

