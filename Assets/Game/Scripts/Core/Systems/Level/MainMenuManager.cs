using UnityEngine;
using VehicleGame.Core.Events;
using VehicleGame.Core.UI.HUD;
using Zenject;

namespace VehicleGame.Core.Systems.Level
{
    public class MainMenuManager : MonoBehaviour
    {
        private CoinsViewModel _coinsViewModel;
        private SignalBus _signalBus;
        private ChangeScene _changeScene;

        [Inject]
        private void Initialize(CoinsViewModel coinsViewModel, SignalBus signalBus, ChangeScene changeScene)
        {
            _changeScene = changeScene;
            _coinsViewModel = coinsViewModel;
            _signalBus = signalBus;

            _signalBus.Subscribe<ChangeLevelSignal>(ChangeLevel);
        }

        private void Start()
        {
            _coinsViewModel.NotifyCoinsChanged();
        }

        private void ChangeLevel(ChangeLevelSignal signal)
        {
            _changeScene.ChangeSceneAsync(signal._levelName);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ChangeLevelSignal>(ChangeLevel);
        }
    }
}

