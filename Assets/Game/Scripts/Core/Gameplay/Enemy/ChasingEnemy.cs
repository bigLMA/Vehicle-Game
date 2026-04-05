using UnityEngine;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.Events;
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
        private SignalBus _signalBus;

        private EnemyStateManager _stateManager;

        [Inject]
        public void Initialize(EnemyConfig config, IVehicle vehicle, SignalBus signalBus)
        {
            _target = vehicle;
            _enemyConfig = config;
            _signalBus = signalBus;
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
            // CHANGED ORDER
            _stateManager.Exit();

            if(_health.current==0)
            {
                _signalBus.Fire(new EnemyKilledSignal(_enemyConfig.bounty));
            }

            base.OnDeath();
        }

        private void OnCollison()
        {
            OnEnemyDeath?.Invoke(this);
        }

        private void OnHealthChange(int current, int max)
        {
            if (current < max)
            {
                _stateManager.EnterChase();
            }
        }
    }

}

