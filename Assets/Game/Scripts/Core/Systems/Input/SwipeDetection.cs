using UnityEngine;
using VehicleGame.Core.Events;
using VehicleGame.Core.Interfaces;
using Zenject;

namespace VehicleGame.Core.Systems.Input
{
    public class SwipeDetection : MonoBehaviour
    {
        private ISwipeReceiver _swipeReceiver;
        private SignalBus _signalBus;
        private InputService _inputService;

        private bool _touching = false;

        private float _lastPositionX;

        private Vector2 _currentPosition => _inputService._swipe;

        private void Awake()
        {
            _signalBus.Subscribe<PauseGameSignal>(Disable);
            _signalBus.Subscribe<ResumeGameSignal>(Enable);

            _inputService.OnPress += OnPress;

            Disable();
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<PauseGameSignal>(Disable);
            _signalBus.Unsubscribe<ResumeGameSignal>(Enable);

            _inputService.OnPress -= OnPress;
        }

        [Inject]
        public void Initialize(ISwipeReceiver swipeReceiver, SignalBus signalBus, InputService inputService)
        {
            _inputService = inputService;
            _swipeReceiver = swipeReceiver;
            _signalBus = signalBus;
        }

        private void Update()
        {
            if (!_touching) return;

            float currentX = _currentPosition.x;
            float delta = currentX - _lastPositionX;

            _lastPositionX = currentX;

            _swipeReceiver.ReceiveSwipe(delta);
        }

        private void OnPress(bool pressed)
        {
            _touching = pressed;

            if(pressed)
            {
                _lastPositionX = _currentPosition.x;
            }
        }

        private void Enable() => enabled = true;
        private void Disable() => enabled = false;
    }
}
