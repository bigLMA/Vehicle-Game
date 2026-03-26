using UnityEngine;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.Interfaces;
using Zenject;

namespace VehicleGame.Core.Gameplay.Enemy
{
    public class ChasingEnemy : Enemy
    {
        private EnemyConfig _enemyConfig;
        private IMovable _move;
        private IVehicle _target;
        private EnemyCollisionHandler _collisionHandler;

        private EnemyStateManager _stateManager;

        [Inject]
        public void Initialize(EnemyConfig config, IVehicle vehicle)
        {
            _target = vehicle;
            _enemyConfig = config;
        }

        protected override void Awake()
        {
            base.Awake();

            _collisionHandler = GetComponent<EnemyCollisionHandler>();
            _collisionHandler.Configure(_target, _enemyConfig.damage);
            _collisionHandler.OnCollisionWithVehicle += OnCollison;

            var animator = GetComponent<Animator>();
            _health.Setup(_enemyConfig.maxHealth);
            _health.OnHealthChange += OnHealthChange;

            _move = GetComponent<IMovable>();

            _stateManager = new(_move, _target.GetTransform(), animator, _enemyConfig);
        }

        public override void Construct(Vector3 pos)
        {
            base.Construct(pos);

            _health.ResetHealth();
            _stateManager.EnterWandering();
        }

        protected override void OnDestroy()
        {
            _stateManager.Exit();
            base.OnDestroy();
        }

        private void Update()
        {
            _stateManager.Tick(Time.deltaTime);
        }

        protected override void OnDeath()
        {
            base.OnDeath();

            _stateManager.Exit();
        }

        private void OnCollison()
        {
            OnEnemyDeath?.Invoke(this);
        }

        private void OnHealthChange(int arg1, int arg2)
        {
            if (arg1 < arg2)
            {
                _stateManager.EnterChase();
            }
        }
    }

}

