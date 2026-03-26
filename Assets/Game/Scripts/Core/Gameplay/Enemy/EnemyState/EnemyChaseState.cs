using UnityEngine;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.Interfaces;

namespace VehicleGame.Core.Gameplay.Enemy
{
    public class EnemyChaseState : EnemyState
    {
        private IMovable _move;
        private EnemyConfig _config;
        private Transform _target;
        private Animator _animator;

        public EnemyChaseState(IMovable move, Transform target, Animator animator, EnemyConfig config)
        {
            _animator = animator;
            _target = target;
            _move = move;
            _config = config;
        }

        public override void Enter()
        {
            _move.StartMoving();
            _move.SetSpeed(_config.chaseSpeed);

            _animator.SetFloat("CurrentSpeed", _config.chaseSpeed);
        }

        public override void Exit()
        {
            _animator.SetFloat("CurrentSpeed", 0f);
        }

        public override void Tick(float deltaTime)
        {
            var direction = (_target.position - _move.GetTransform().position).normalized;
            _move.SetDirection(direction);
        }
    }
}

