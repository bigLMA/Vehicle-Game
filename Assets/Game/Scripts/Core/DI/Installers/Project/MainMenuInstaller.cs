using UnityEngine;
using VehicleGame.Core.Events;
using VehicleGame.Core.Gameplay.Vehicle;
using VehicleGame.Core.Systems.Level;
using VehicleGame.Core.UI.HUD;
using Zenject;

namespace VehicleGame.Core.DI
{
    public class MainMenuInstaller : MonoInstaller
    {
        [SerializeField]
        private VehicleUpgradeConfig _vehicleUpgradeConfig;

        public override void InstallBindings()
        {
            Container.Bind<CoinsViewModel>().AsSingle().NonLazy();
            Container.Bind<ChangeScene>().AsSingle().NonLazy();
            Container.Bind<VehicleUpgradeViewModel>().AsSingle().NonLazy(); 
            Container.Bind<VehicleUpgradeConfig>().FromInstance( _vehicleUpgradeConfig );

            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<CoinsChangedSignal>();
            Container.DeclareSignal<PurchasedSignal>();
            Container.DeclareSignal<ChangeLevelSignal>();
            Container.DeclareSignal<DragUpdateStartedSignal>();
            Container.DeclareSignal<DragUpdateEndedSignal>();
        }
    }
}
