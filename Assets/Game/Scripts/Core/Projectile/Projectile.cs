using UnityEngine;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.Interfaces;
using Zenject;

namespace VehicleGame.Core.Gameplay.Projectile
{
    public class Projectile : MonoBehaviour
    {
        private float _lifeTimer;

        private IMemoryPool _pool;

        private ProjectileConfig _config;

        private TrailRenderer _trailRenderer;
        private IMovable _move;

        private void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
            _move = GetComponent<IMovable>();
            _move.SetSpeed(_config.speed);
        }

        [Inject]
        private void Initialize(ProjectileConfig config)
        {
            _config = config;
        }

        public void Construct(Vector3 position, Vector3 direction, IMemoryPool pool)
        {
            _trailRenderer.Clear();
            _trailRenderer.enabled = false;
            _trailRenderer.enabled = true;

            _lifeTimer = _config.lifeTimer;
            transform.position = position;
            transform.forward = direction;
            _pool = pool;
            gameObject.SetActive(true);

            _move.SetDirection(direction);
            _move.StartMoving();
        }

        private void LateUpdate()
        {
            _lifeTimer -= Time.deltaTime;

            if (_lifeTimer < 0)
                DespawnSelf();
        }

        public void DespawnSelf()
        {
            _move.Stop();

            if (gameObject.activeSelf)
            {
                _pool.Despawn(this);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<IDamageable>(out var damagable))
            {
                damagable.TakeDamage(_config.damage);
            }

            DespawnSelf();
        }
    }
}