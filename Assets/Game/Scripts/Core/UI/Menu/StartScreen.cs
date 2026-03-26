using UnityEngine;
using VehicleGame.Core.Events;
using VehicleGame.Core.Interfaces;
using Zenject;

namespace VehicleGame.Core.UI.Menu
{
    public class StartScreen : MonoBehaviour, ITapReceiver
    {
        // Dependencies
        private SignalBus _signalBus;

        [Inject]
        public void Initialize(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<ResetLevelSignal>(Show);
            _signalBus.Subscribe<StartLevelSignal>(Hide);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ResetLevelSignal>(Show);
            _signalBus.Unsubscribe<StartLevelSignal>(Hide);
        }

        public void ReceiveTap()
        {
            _signalBus?.Fire(new StartLevelSignal());
        }

        private void Show() => gameObject.SetActive(true);
        private void Hide() => gameObject.SetActive(false);
    }
}
