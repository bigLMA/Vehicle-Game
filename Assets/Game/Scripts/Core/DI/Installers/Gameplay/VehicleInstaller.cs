using UnityEngine;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.Gameplay.Projectile;
using VehicleGame.Core.Gameplay.Vehicle;
using VehicleGame.Core.Interfaces;
using VehicleGame.Core.Move;
using Zenject;

namespace VehicleGame.Core.DI
{
    public class VehicleInstaller : MonoInstaller
    {
        [Header("Vehicle")]
        [SerializeField]
        private Vehicle _vehicle;
        [SerializeField]
        private VehicleConfig _vehicleConfig;

        [Header("Turret")]
        [SerializeField]
        private TurretConfig _turretConfig;
        [SerializeField]
        private Rotate _turret;

        [Header("Projectile")]
        [SerializeField]
        private Projectile _projectilePrefab;
        [SerializeField]
        private ProjectileConfig _projectileConfig;

        [Header("Upgrades")]
        [SerializeField]
        private VehicleUpgradeConfig _vehicleUpgradeConfig;


        public override void InstallBindings()
        {
            Container.Bind<IVehicle>().FromInstance(_vehicle).AsSingle();
            Container.Bind<VehicleConfig>().FromInstance(_vehicleConfig).AsSingle();
            Container.Bind<TurretConfig>().FromInstance(_turretConfig).AsSingle();
            Container.Bind<ProjectileConfig>().FromInstance(_projectileConfig).AsSingle();
            Container.Bind<ISwipeReceiver>().FromInstance(_turret).AsSingle(); 
            Container.Bind<VehicleUpgradeConfig>().FromInstance(_vehicleUpgradeConfig);

            // Pools
            Container.BindMemoryPool<Projectile, ProjectilePool>()
                .WithInitialSize(25)
                .FromComponentInNewPrefab(_projectilePrefab)
                .UnderTransformGroup("Projectiles");
        }
    }
}
