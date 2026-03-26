using UnityEngine;
using TMPro;
using Zenject;
using UnityEngine.UI;
using VehicleGame.Core.Events;

namespace VehicleGame.Core.UI.Menu
{
    public class FinalScreen : MonoBehaviour
    {
        private SignalBus _signalBus;

        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private Button _quitButton;
        [SerializeField]
        private TextMeshProUGUI _gameResultTextField;

        private void Awake()
        {
            _restartButton.onClick.AddListener(() =>
            {
                _signalBus.Fire(new ResetLevelSignal());
                Hide();
            });

            _quitButton.onClick.AddListener(() =>
            {
                _signalBus.Fire(new QuitGameSignal());
            });

            Hide();
        }

        private void OnDestroy()
        {
            _restartButton.onClick.RemoveAllListeners();
            _quitButton.onClick.RemoveAllListeners();
        }

        [Inject]
        public void Initialize(SignalBus signalBus)
        {
            _signalBus = signalBus;

            _signalBus.Subscribe<GameWonSignal>(() =>
            {
                gameObject.SetActive(true);
                GameWon();
            });

            _signalBus.Subscribe<GameLostSignal>(() =>
            {
                gameObject.SetActive(true);
                GameLost();
            });

            _signalBus.Subscribe<StartLevelSignal>(Hide);
        }

        public void GameWon()
        {
            _gameResultTextField.text = "You Won!";
        }

        public void GameLost()
        {
            _gameResultTextField.text = "You Lost!";
        }

        public void Display() => gameObject.SetActive(true);
        public void Hide() => gameObject.SetActive(false);
    }
}
