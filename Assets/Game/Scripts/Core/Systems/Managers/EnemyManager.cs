using System.Collections.Generic;
using UnityEngine;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.DI;
using VehicleGame.Core.Events;
using VehicleGame.Core.Gameplay.Enemy;
using Zenject;

namespace VehicleGame.Core.Systems.Managers
{
    public class EnemyManager : MonoBehaviour
    {
        private SignalBus _signalBus;
        private LevelConfig _levelConfig;
        private EnemyPool _enemyPool;

        private List<Enemy> _enemyList;


        [Inject]
        private void Initialize(SignalBus signalBus, EnemyPool enemyPool, LevelConfig levelConfig)
        {
            _signalBus = signalBus;
            _enemyPool = enemyPool;
            _levelConfig = levelConfig;
        }

        private void Awake()
        {
            _enemyList = new();

            _signalBus.Subscribe<ResetLevelSignal>(SpawnEnemies);
            _signalBus.Subscribe<GameWonSignal>(ClearEnemies);
            _signalBus.Subscribe<GameLostSignal>(ClearEnemies);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ResetLevelSignal>(SpawnEnemies);
            _signalBus.Unsubscribe<GameWonSignal>(ClearEnemies);
            _signalBus.Unsubscribe<GameLostSignal>(ClearEnemies);
        }

        public void SpawnEnemies()
        {
            float zCoord;

            float xCoord;
            var yCoord = 0f;

            for (int i = 0; i < _levelConfig.enemiesAmount; ++i)
            {
                zCoord = Random.Range(_levelConfig.zSpawnStart, _levelConfig.zSpawnEnd);
                xCoord = Random.Range(-_levelConfig.roadWidth / 2, _levelConfig.roadWidth / 2);

                var enemy = _enemyPool.Spawn(new Vector3(xCoord, yCoord, zCoord));
                enemy.OnEnemyDeath += OnEnemyDeath;
                _enemyList.Add(enemy);
            }
        }

        public void ClearEnemies()
        {
            foreach (var enemy in _enemyList)
            {
                enemy.OnEnemyDeath -= OnEnemyDeath;
                _enemyPool.Despawn(enemy);
            }

            _enemyList.Clear();
        }

        private void OnEnemyDeath(Enemy enemy)
        {
            _enemyList.Remove(enemy);
            enemy.OnEnemyDeath -= OnEnemyDeath;
            _enemyPool.Despawn(enemy);
        }
    }

}