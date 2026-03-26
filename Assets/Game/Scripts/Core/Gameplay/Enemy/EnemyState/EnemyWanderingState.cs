using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.Interfaces;

namespace VehicleGame.Core.Gameplay.Enemy
{
    public class EnemyWanderingState : EnemyState
    {
        private IMovable _move;
        private EnemyStateManager _stateManager;
        private EnemyConfig _config;
        private Transform _target;
        private Animator _animator;

        private CancellationTokenSource _cts;

        private Vector3 _targetPosition;

        public EnemyWanderingState(IMovable move, Transform target, Animator animator, EnemyConfig config, EnemyStateManager stateMachine)
        {
            _animator = animator;
            _target = target;
            _config = config;
            _stateManager = stateMachine;
            _move = move;
        }

        public override void Enter()
        {
            _cts = new();
            _move.SetSpeed(_config.wanderSpeed);

            // Start Wandering
            Wander(_cts.Token).Forget();
        }

        public override void Exit()
        {
            _cts?.Cancel();
            _cts?.Dispose();
        }

        public override void Tick(float deltaTime)
        {
            // Check for target proximity
            if (Vector3.Distance(_move.GetTransform().position, _target.transform.position) < _config.chaseDistance)
            {
                // Change state if close enough to the target
                _stateManager.EnterChase();
                return;
            }
        }

        private async UniTask Wander(CancellationToken token)
        {
            try
            {
                // Initial random delay
                await UniTask.WaitForSeconds(Random.Range(0f, _config.initialWanderingDelay), cancellationToken: token);

                while (!token.IsCancellationRequested)
                {
                    // Pause between wanderings
                    _move.Stop();
                    _animator.SetFloat("CurrentSpeed", 0f);
                    await UniTask.WaitForSeconds(Random.Range(0.1f, _config.pauseTime), cancellationToken: token);

                    if (token.IsCancellationRequested) throw new System.OperationCanceledException();

                    var pos = GetRandomPosition();

                    _move.StartMoving();
                    _animator.SetFloat("CurrentSpeed", _config.wanderSpeed);

                    if (token.IsCancellationRequested) throw new System.OperationCanceledException();

                    // Wander
                    while (Vector3.Distance(pos, _move.GetTransform().position) > 0.1f)
                    {
                        var direction = (pos - _move.GetTransform().position).normalized;
                        _move.SetDirection(direction);
                        await UniTask.Yield(token);
                    }
                }
            }
            catch (System.OperationCanceledException) { }
        }

        private Vector3 GetRandomPosition()
        {
            var offset = Random.insideUnitCircle * _config.wanderRadius;
            var x = Mathf.Clamp(offset.x + _move.GetTransform().position.x, -_config.xBounds, _config.xBounds);
            var pos = new Vector3(x, 0f, _move.GetTransform().transform.position.z + offset.y);

            return pos;
        }
    }
}