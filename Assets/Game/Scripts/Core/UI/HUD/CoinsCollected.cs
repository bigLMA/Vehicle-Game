using TMPro;
using UnityEngine;
using VehicleGame.Core.Events;
using Zenject;

namespace VehicleGame.Core.UI.HUD
{
    public class CoinsCollected : MonoBehaviour
    {
        private SignalBus _signalBus;

        [SerializeField]
        private TextMeshProUGUI coinsText;

        [Inject]
        public void Initialize(SignalBus signalBus)
        {
            _signalBus = signalBus;
            _signalBus.Subscribe<CoinsChangedSignal>(OnCoinsChanged);
        }

        private void OnCoinsChanged(CoinsChangedSignal signal)
        {
            coinsText.text = signal.current.ToString();
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<CoinsChangedSignal>(OnCoinsChanged);
        }
    }
}

