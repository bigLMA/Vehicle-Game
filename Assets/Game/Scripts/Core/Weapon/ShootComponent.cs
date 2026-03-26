using UnityEngine;
using VehicleGame.Core.DI;
using VehicleGame.Core.Interfaces;
using Zenject;

namespace VehicleGame.Core.Weapon
{
    public class ShootComponent : MonoBehaviour, IWeapon
    {
        [SerializeField]
        private MuzzleFlash _muzzleFlash;

        private float _initialDelay;
        private float _shootInterval;
        private Transform _muzzlePoint;

        private bool _isShooting = false;
        private float _shootTimer;

        private LineRenderer _lineRenderer;

        private float _lineLength;
        private ProjectilePool _projectilePool;

        [Inject]
        private void Initialize(ProjectilePool pool)
        {
            _projectilePool = pool;
        }

        private void Awake()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.widthCurve.ClearKeys();
            _lineRenderer.widthCurve.AddKey(0f, 0.001f);
        }

        public void Configure(float initialDelay, float shootInterval, Transform muzzlePoint, float lineLength)
        {
            _initialDelay = initialDelay;
            _shootInterval = shootInterval;
            _muzzlePoint = muzzlePoint;
            _shootTimer = _initialDelay;
            _lineLength = lineLength;
        }

        public void ResetWeapon()
        {
            _shootTimer = _initialDelay;
        }

        private void LateUpdate()
        {
            if (_isShooting)
            {
                _shootTimer -= Time.deltaTime;

                if (_shootTimer < 0)
                {
                    Shoot();
                }
            }

            _lineRenderer.SetPosition(0, _muzzlePoint.position);
            _lineRenderer.SetPosition(1, _muzzlePoint.position + _muzzlePoint.forward * _lineLength);
        }

        public void Shoot()
        {
            _muzzleFlash.Play();
            _projectilePool.Spawn(_muzzlePoint.position, _muzzlePoint.forward);
            _shootTimer = _shootInterval;
        }

        public void StartShooting() => _isShooting = true;
        public void StopShooting() => _isShooting = false;
    }
}

