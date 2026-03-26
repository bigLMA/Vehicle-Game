using UnityEngine;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.Interfaces;

namespace VehicleGame.Core.Gameplay.Enemy
{
    public class EnemyStateManager
    {
        private EnemyState _currentState;
        private readonly EnemyState _wanderingState;
        private readonly EnemyState _chaseState;

        public EnemyStateManager(IMovable move, Transform target, Animator animator, EnemyConfig config)
        {
            _chaseState = new EnemyChaseState(move, target, animator, config);
            _wanderingState = new EnemyWanderingState(move, target, animator, config, this);
        }

        public void Tick(float deltaTime) => _currentState?.Tick(deltaTime);

        public void ChangeState(EnemyState newState)
        {
            if (newState == _currentState) return;

            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }

        public void EnterWandering() => ChangeState(_wanderingState);

        public void EnterChase() => ChangeState(_chaseState);

        public void Exit()
        {
            _currentState?.Exit();
            _currentState = null;
        }
    }
}

