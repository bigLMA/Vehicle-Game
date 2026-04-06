using VehicleGame.Core.Events;
using VehicleGame.Core.Systems.Level;
using VehicleGame.Core.UI.HUD;
using Zenject;

namespace VehicleGame.Core.DI
{
    public class MainMenuInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CoinsViewModel>().AsSingle().NonLazy();
            Container.Bind<ChangeScene>().AsSingle().NonLazy();

            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<CoinsChangedSignal>();
            Container.DeclareSignal<ChangeLevelSignal>();
        }
    }
}
