using UnityEngine;
using VehicleGame.Core.Data.Configs;
using VehicleGame.Core.Events;
using VehicleGame.Core.PlayerStats;
using VehicleGame.Core.Systems.GameManager;
using VehicleGame.Core.Systems.Input;
using Zenject;

namespace VehicleGame.Core.DI
{
    public class GameStateInstaller : MonoInstaller
    {
        [SerializeField]
        private GameStateManager _stateManager;

        [SerializeField]
        private LevelConfig _levelConfig;

        public override void InstallBindings()
        {
            Container.Bind<GameStateManager>().FromInstance(_stateManager).AsSingle();
            Container.Bind<LevelConfig>().FromInstance(_levelConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerKillCount>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<InputService>().AsSingle().NonLazy();

            InstallSignalBus();
        }

        private void InstallSignalBus()
        {
            SignalBusInstaller.Install(Container);

            // Register signals
            Container.DeclareSignal<PauseGameSignal>();
            Container.DeclareSignal<StartLevelSignal>();
            Container.DeclareSignal<ResumeGameSignal>();
            Container.DeclareSignal<GameLostSignal>();
            Container.DeclareSignal<GameWonSignal>();
            Container.DeclareSignal<ResetLevelSignal>();
            Container.DeclareSignal<QuitGameSignal>();
            Container.DeclareSignal<EnemyKilledSignal>();
        }
    }
}
