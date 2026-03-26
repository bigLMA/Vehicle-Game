using UnityEngine;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.Events;
using VehicleGame.Core.Interfaces;
using Zenject;

namespace VehicleGame.Core.Systems.Level
{
    public class GroundLoop : MonoBehaviour
    {
        private Transform _groundPrefab;
        private float _groundSpawnInterval;
        private IVehicle _vehicle;
        private SignalBus _signalBus;

        private Transform _first;
        private Transform _second;

        private float _nextSpawnZ;
        private bool _moveFirst = true;

        [SerializeField]
        private float initialSpawnOffset = 10f;

        [Inject]
        private void Initialize(LevelConfig levelConfig, IVehicle vehicle, SignalBus signalBus)
        {
            _groundPrefab = levelConfig.groundPrefab.transform;
            _groundSpawnInterval = levelConfig.groundSpawnInterval;
            _vehicle = vehicle;
            _signalBus = signalBus;
        }

        void Start()
        {
            _first = Instantiate(_groundPrefab, Vector3.zero, Quaternion.identity, transform);
            _second = Instantiate(_groundPrefab, new(0f, 0f, _groundSpawnInterval), Quaternion.identity, transform);

            _signalBus.Subscribe<ResetLevelSignal>(ResetGround);

            _nextSpawnZ = _groundSpawnInterval - initialSpawnOffset;
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ResetLevelSignal>(ResetGround);
        }

        private void Update()
        {
            if (_vehicle.GetTransform().position.z >= _nextSpawnZ)
            {
                MoveGround();
            }
        }

        private void MoveGround()
        {
            var target = _moveFirst ? _second : _first;
            var moving = _moveFirst ? _first : _second;

            moving.position = new Vector3(0f, 0f, target.position.z + _groundSpawnInterval);

            _nextSpawnZ += _groundSpawnInterval;
            _moveFirst = !_moveFirst;
        }

        private void ResetGround()
        {
            _moveFirst = true;
            _first.transform.position = Vector3.zero;
            _second.transform.position = new(0f, 0f, _groundSpawnInterval);

            _nextSpawnZ = _groundSpawnInterval - initialSpawnOffset;
        }
    }
}