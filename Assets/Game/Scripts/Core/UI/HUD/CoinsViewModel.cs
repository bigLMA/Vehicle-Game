using System;
using UnityEngine;
using VehicleGame.Core.Events;
using VehicleGame.Core.PlayerStats;
using VehicleGame.Utils.Data;
using Zenject;

namespace VehicleGame.Core.UI.HUD
{
    public class CoinsViewModel
    {
        private SignalBus _signalBus;
        private SaveData _saveData;
        private LoadData _loadData;
        private ISaveLoadDataProvider _saveLoadDataProvider;

        private PlayerData _playerData;

        [Inject]
        private void Initialize(SignalBus signalBus, SaveData saveData, LoadData loadData, ISaveLoadDataProvider saveLoadDataProvider)
        {
            _saveLoadDataProvider = saveLoadDataProvider;
            _loadData = loadData;
            _saveData = saveData;
            _signalBus = signalBus;

            _playerData = _loadData.GetPlayerData<PlayerData>(_saveLoadDataProvider.GetPlayerDataFileName());
        }

        public bool CheckCoinPurchase(int value)
        {
            return value <= _playerData.coins;
        }

        public void Purchase(int value)
        {
            if (!CheckCoinPurchase(value)) return;

            _playerData.coins-=value;
            _saveData.Save(_playerData, _saveLoadDataProvider.GetPlayerDataFileName());
            NotifyCoinsChanged();
        }

        public void NotifyCoinsChanged()
        {
            _signalBus.Fire(new CoinsChangedSignal(_playerData.coins));
        }
    }
}

