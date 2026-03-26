using UnityEngine;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.Events;
using VehicleGame.Core.Interfaces;
using Zenject;

namespace VehicleGame.Core.Gameplay.Vehicle
{
    public class Vehicle : MonoBehaviour, IVehicle
    {
        private IHealth _health;
        private IMovable _move;

        private VehicleConfig _vehicleConfig;
        private SignalBus _signalBus;

        private void Awake()
        {
            // Initialize health
            _health = GetComponent<IHealth>();
            _health.Setup(_vehicleConfig.vehicleHealthPoints);

            // Initialize movable
            _move = GetComponent<IMovable>();
            _move.SetSpeed(_vehicleConfig.vehicleSpeed);
            _move.SetDirection(Vector3.forward);

            _health.OnDeath += OnDeath;

            _signalBus.Subscribe<ResetLevelSignal>(ResetVehicle);
            _signalBus.Subscribe<StartLevelSignal>(LaunchVehicle);
        }

        private void OnDestroy()
        {
            _health.OnDeath -= OnDeath;
        }

        [Inject]
        public void Initialize(VehicleConfig vehicleConfig, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _vehicleConfig = vehicleConfig;
        }

        public Transform GetTransform() => transform;

        public void ResetVehicle()
        {
            transform.position = Vector3.zero;
            _health.ResetHealth();
            _move.Stop();
        }

        private void LaunchVehicle()
        {
            _move.StartMoving();
        }

        private void OnDeath()
        {
            _signalBus.Fire(new GameLostSignal());
        }

        public void TakeDamage(int damage) => _health.TakeDamage(damage);
    }
}
