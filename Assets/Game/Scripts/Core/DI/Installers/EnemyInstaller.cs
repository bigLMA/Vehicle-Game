using UnityEngine;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.Gameplay.Enemy;
using Zenject;

namespace VehicleGame.Core.DI
{
    public class EnemyInstaller : MonoInstaller
    {
        [SerializeField]
        private EnemyConfig _enemyConfig;

        [SerializeField]
        private Enemy _enemyPrefab;

        public override void InstallBindings()
        {
            Container.Bind<EnemyConfig>().FromInstance(_enemyConfig).AsSingle();

            Container.BindMemoryPool<Enemy, EnemyPool>()
            .WithInitialSize(60)
            .FromComponentInNewPrefab(_enemyPrefab)
            .UnderTransformGroup("Enemies");
        }
    }
}