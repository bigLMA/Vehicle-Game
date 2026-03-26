using UnityEngine;
using VehicleGame.Core.Interfaces;
using VehicleGame.Core.UI.Menu;
using Zenject;

namespace VehicleGame.Core.DI
{
    public class UIInstaller : MonoInstaller
    {
        [SerializeField]
        private StartScreen _startScreen;

        public override void InstallBindings()
        {
            Container.Bind<ITapReceiver>().FromInstance(_startScreen).AsSingle();
        }
    }
}
