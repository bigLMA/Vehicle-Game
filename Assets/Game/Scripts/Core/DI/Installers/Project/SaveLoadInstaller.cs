using UnityEngine;
using VehicleGame.Utils.Data;
using Zenject;

namespace VehicleGame.Core.DI
{
    public class SaveLoadInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ISaveLoadDataProvider>().To<SaveLoadData>().AsSingle();
            Container.Bind<LoadData>().AsSingle().NonLazy();
            Container.Bind<SaveData>().AsSingle().NonLazy();
        }
    }
}
