using UnityEngine;
using UnityEngine.InputSystem;
using VehicleGame.Core.Events;
using VehicleGame.Core.Interfaces;
using Zenject;

namespace VehicleGame.Core.Systems.Input
{
    public class TapDetection : MonoBehaviour
    {
        private ITapReceiver _tapReceiver;
        private SignalBus _signalBus;
        private InputService _inputService;

        [Inject]
        private void Initialize(ITapReceiver tapReceiver, SignalBus signalBus, InputService inputService)
        {
            _signalBus = signalBus;
            _tapReceiver = tapReceiver;
            _inputService = inputService;

            _signalBus.Subscribe<StartLevelSignal>(Deactivate);
            _signalBus.Subscribe<ResetLevelSignal>(Activate);

            _inputService.OnTap += OnTap;
        }

        public void OnTap()
        {
            if (_tapReceiver != null)
                _tapReceiver.ReceiveTap();
        }

        private void Activate()
        {
            _inputService.OnTap += OnTap;
        }

        private void Deactivate()
        {
            _inputService.OnTap -= OnTap;
        }
    }
}

