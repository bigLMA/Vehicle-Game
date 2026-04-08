using UnityEngine;
using VehicleGame.Core.PlayerStats;
using VehicleGame.Utils.Data;
using Zenject;

namespace VehicleGame.Core.Gameplay.Vehicle
{
    public class VehicleUpgradeViewModel
    {
        private SignalBus _signalBus;
        private SaveData _saveData;
        private LoadData _loadData;
        private ISaveLoadDataProvider _saveLoadDataProvider;
        private VehicleUpgradeConfig _vehicleUpgradeConfig;

        VehicleUpgradeData _vehicleUpgradeData;

        [Inject]
        private void Initialize(SignalBus signalBus,
            SaveData saveData, 
            LoadData loadData, 
            ISaveLoadDataProvider saveLoadDataProvider,
            VehicleUpgradeConfig vehicleUpgradeConfig)
        {
            _vehicleUpgradeConfig = vehicleUpgradeConfig;
            _saveLoadDataProvider = saveLoadDataProvider;
            _loadData = loadData;
            _saveData = saveData;
            _signalBus = signalBus;

            _vehicleUpgradeData = _loadData.Load<VehicleUpgradeData>(_saveLoadDataProvider.GetVehicleDataFileName());
        }

        public int GetCurrentUpgradeCost()
        {
            return _vehicleUpgradeConfig.initialPrice 
                + (_vehicleUpgradeData.turretUpgrades + _vehicleUpgradeData.frameUpgrades + _vehicleUpgradeData.wheelUpgrades) 
                * _vehicleUpgradeConfig.priceIncrease;
        }

        public void Upgrade(VehicleUpgrader upgrader)
        {
            upgrader.Upgrade(_vehicleUpgradeData);
            SaveUpgradeData();
        }

        private void SaveUpgradeData()
        {
            _saveData.Save(_vehicleUpgradeData, _saveLoadDataProvider.GetVehicleDataFileName());
        }
    }
}

