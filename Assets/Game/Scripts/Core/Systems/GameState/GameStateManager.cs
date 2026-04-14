using UnityEditor;
using UnityEngine;
using Zenject;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.Events;

using VehicleGame.Core.Interfaces;
using VehicleGame.Core.Systems.Level;



#if UNITY_EDITOR
using UnityEditor;
#endif

namespace VehicleGame.Core.Systems.GameManager
{
    public class GameStateManager : MonoBehaviour
    {
        private SignalBus _signalBus;
        private LevelConfig _levelConfig;
        private IVehicle _vehicle;
        private ChangeScene _changeScene;

        private void Start()
        {
            _signalBus.Fire(new ResetLevelSignal());
            ResetLevel();
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<PauseGameSignal>(Pause);
            _signalBus.Unsubscribe<ResumeGameSignal>(Resume);
            _signalBus.Unsubscribe<GameLostSignal>(Pause);
            _signalBus.Unsubscribe<StartLevelSignal>(StartLevel);
            _signalBus.Unsubscribe<ResetLevelSignal>(ResetLevel);
            _signalBus.Unsubscribe<QuitGameSignal>(QuitGame);
            _signalBus.Unsubscribe<ChangeLevelSignal>(ChangeScene);
        }

        [Inject]
        public void Initialize(SignalBus signalBus, LevelConfig levelConfig, IVehicle vehicle, ChangeScene changeScene)
        {
            _changeScene = changeScene;
            _levelConfig = levelConfig;
            _signalBus = signalBus;
            _vehicle = vehicle;

            _signalBus.Subscribe<PauseGameSignal>(Pause);
            _signalBus.Subscribe<ResumeGameSignal>(Resume);
            _signalBus.Subscribe<GameLostSignal>(Pause);
            _signalBus.Subscribe<StartLevelSignal>(StartLevel);
            _signalBus.Subscribe<ResetLevelSignal>(ResetLevel);
            _signalBus.Subscribe<QuitGameSignal>(QuitGame);
            _signalBus.Subscribe<ChangeLevelSignal>(ChangeScene);
        }

        private void Update()
        {
            if (_vehicle.GetTransform().position.z >= _levelConfig.endPoint.z && Time.timeScale != 0f)
            {
                _signalBus.Fire(new GameWonSignal());
                _signalBus.Fire(new PauseGameSignal());
            }
        }

        public void Resume()
        {
            Time.timeScale = 1f;
        }

        public void Pause()
        {
            Time.timeScale = 0f;
        }

        private void ResetLevel()
        {
            //
            Pause();
        }

        private void StartLevel()
        {
            _signalBus?.Fire(new ResumeGameSignal());
        }

        private void ChangeScene(ChangeLevelSignal signal)
        {
            _changeScene.LoadScene(signal._levelName);
        }

        private void QuitGame()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }
}

