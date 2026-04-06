using VehicleGame.Core.Events;
using VehicleGame.Core.UI.HUD;
using Zenject;

namespace VehicleGame.Core.DI
{
    public class MainMenuInstaller :MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<CoinsViewModel>().AsSingle().NonLazy();

            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<CoinsChangedSignal>();
        }
    }
}
