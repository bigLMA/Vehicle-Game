using System;
using UnityEngine;
using VehicleGame.Core.Events;
using VehicleGame.Utils.Data;
using Zenject;

namespace VehicleGame.Core.PlayerStats
{
    public class PlayerKillCount : IDisposable
    {
        private int count = 0;

        private SignalBus _signalBus;
        private LoadData _loadData;
        private SaveData _saveData;
        private ISaveLoadDataProvider _saveDataProvider;

        [Inject]
        private void Initialize(SignalBus signalBus, LoadData loadData, SaveData saveData, ISaveLoadDataProvider saveLoadDataProvider)
        {
            _saveDataProvider = saveLoadDataProvider;
            _signalBus = signalBus;
            _loadData = loadData;
            _saveData = saveData;

            _signalBus.Subscribe<EnemyKilledSignal>(EnemyKilled);
            _signalBus.Subscribe<GameWonSignal>(GameEnded);
            _signalBus.Subscribe<GameLostSignal>(GameEnded);
            _signalBus.Subscribe<ResetLevelSignal>(ResetCount);
        }

        private void EnemyKilled(EnemyKilledSignal signal)
        {
            count += signal.value;
            _signalBus.Fire(new CoinsChangedSignal(count));
        }

        private void GameEnded()
        {
            var data = _loadData.GetPlayerData<PlayerData>(_saveDataProvider.GetPlayerDataFileName()) ?? new PlayerData();
            data.coins += count;
            _saveData.Save(data, _saveDataProvider.GetPlayerDataFileName());
        }

        private void ResetCount()
        {
            count = 0; 
            _signalBus.Fire(new CoinsChangedSignal(count));
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameWonSignal>(GameEnded);
            _signalBus.Unsubscribe<GameLostSignal>(GameEnded);
            _signalBus.Unsubscribe<EnemyKilledSignal>(EnemyKilled);
        }
    }
}