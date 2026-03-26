using UnityEngine;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.DI;
using VehicleGame.Core.Events;
using VehicleGame.Core.Interfaces;
using VehicleGame.Core.Move;
using Zenject;

namespace VehicleGame.Core.Gameplay.Turret
{
    public class Turret : MonoBehaviour
    {
        [SerializeField]
        private Transform _muzzlePoint;

        private IWeapon _weapon;
        private Rotate _rotate;

        private SignalBus _signalBus;
        private TurretConfig _config;
        private ProjectilePool _projectilePool;

        [Inject]
        private void Initialize(TurretConfig config, ProjectilePool pool, SignalBus signalBus)
        {
            _config = config;
            _projectilePool = pool;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _weapon = GetComponent<IWeapon>();
            _weapon.Configure(_config.initialShootDelay, _config.shootInterval, _muzzlePoint, _config.targetLineLength);

            _rotate = GetComponent<Rotate>();
            _rotate.Configure(_config.sensitivity, _config.damping, _config.maxRotationSpeed);

            _signalBus.Subscribe<ResetLevelSignal>(OnLevelReset);
            _signalBus.Subscribe<ResetLevelSignal>(OnLevelStart);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ResetLevelSignal>(OnLevelReset);
            _signalBus.Unsubscribe<ResetLevelSignal>(OnLevelStart);
        }

        private void OnLevelReset()
        {
            _weapon.ResetWeapon();
            _weapon.StopShooting();

        }

        private void OnLevelStart()
        {
            _weapon.StartShooting();
        }
    }
}

