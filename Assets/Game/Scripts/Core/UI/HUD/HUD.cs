using UnityEngine;
using VehicleGame.Core.Events;
using Zenject;

namespace VehicleGame.Core.UI.HUD
{
    public class HUD : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        private void Initialize(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _signalBus.Subscribe<StartLevelSignal>(Show);
            _signalBus.Subscribe<GameWonSignal>(Hide);
            _signalBus.Subscribe<GameLostSignal>(Hide);
        }
        private void Start()
        {
            Hide();
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<GameWonSignal>(Hide);
            _signalBus.Unsubscribe<GameLostSignal>(Hide);
            _signalBus.Unsubscribe<StartLevelSignal>(Show);
        }

        public void Show() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}

