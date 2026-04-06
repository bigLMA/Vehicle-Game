using UnityEngine;
using UnityEngine.UI;
using VehicleGame.Core.Events;
using Zenject;
namespace VehicleGame.Core.UI.Buttons
{
    public class ChangeLevelButton : MonoBehaviour
    {
        [SerializeField]
        private string _levelName;
        
        private Button _button;
        private SignalBus _signalBus;

        [Inject]
        private void Initialize(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        void Start()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(() =>
            {
                _signalBus.Fire(new ChangeLevelSignal(_levelName));
            });
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}

