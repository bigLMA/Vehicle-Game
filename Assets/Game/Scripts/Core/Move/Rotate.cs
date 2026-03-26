using UnityEngine;
using VehicleGame.Core.Events;
using VehicleGame.Core.Interfaces;
using Zenject;

namespace VehicleGame.Core.Move
{
    public class Rotate : MonoBehaviour, ISwipeReceiver
    {
        private float _sensitivity;
        private float _damping;
        private float _maxRotationSpeed;
        private float _yawVelocity = 0f;

        private SignalBus _signalBus;

        [Inject]
        private void Initialize(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<ResetLevelSignal>(ResetSwipe);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ResetLevelSignal>(ResetSwipe);
        }

        private void Update()
        {
            _yawVelocity = Mathf.Clamp(_yawVelocity, -_maxRotationSpeed, _maxRotationSpeed);

            _yawVelocity = Mathf.Lerp(_yawVelocity, 0f, _damping * Time.deltaTime);

            transform.Rotate(Vector3.up, _yawVelocity * Time.deltaTime);
        }

        public void Configure(float sensivity, float damping, float maxRotationSpeed)
        {
            _sensitivity = sensivity;
            _damping = damping;
            _maxRotationSpeed = maxRotationSpeed;
        }

        public void ReceiveSwipe(float delta)
        {
            _yawVelocity += delta * _sensitivity;
        }

        public void ResetSwipe()
        {
            _yawVelocity = 0f;
            transform.rotation = Quaternion.identity;
        }
    }
}