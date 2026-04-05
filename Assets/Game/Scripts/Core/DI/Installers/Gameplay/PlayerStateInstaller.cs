using VehicleGame.Core.Events;
using VehicleGame.Core.PlayerStats;
using Zenject;

namespace VehicleGame.Core.DI
{
    public class PlayerStateInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerKillCount>().AsSingle().NonLazy();

           Container.DeclareSignal<CoinsChangedSignal>();
        }
    }
}
