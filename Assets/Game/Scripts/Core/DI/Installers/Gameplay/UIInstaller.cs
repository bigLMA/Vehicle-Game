using UnityEngine;
using VehicleGame.Core.Interfaces;
using VehicleGame.Core.Systems.Managers;
using VehicleGame.Core.UI.FloatingUI;
using VehicleGame.Core.UI.Menu;
using Zenject;

namespace VehicleGame.Core.DI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField]
        private StartScreen _startScreen;

        [SerializeField]
        private FloatingUI _notifierPrefab;

        public override void InstallBindings()
        {
            Container.Bind<ITapReceiver>().FromInstance(_startScreen).AsSingle();
            Container.BindInterfacesAndSelfTo<GoldAddedNotifyManager>().AsSingle().NonLazy();

            Container.BindMemoryPool<FloatingUI, AddCoinsNotifierPool>()
                .WithInitialSize(20)
                .FromComponentInNewPrefab(_notifierPrefab)
                .UnderTransformGroup("Notifiers");
        }
    }
}
