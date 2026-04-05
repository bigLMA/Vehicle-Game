using System;
using VehicleGame.Core.Events;
using VehicleGame.Utils.Data;
using Zenject;

namespace VehicleGame.Core.PlayerStats
{
    // TODO CREATE
    public class PlayerKillCount : IDisposable
    {
        private int count = 0;

        private SignalBus _signalBus;
        private LoadData _loadData;
        private SaveData _saveData;

        [Inject]
        private void Initialize(SignalBus signalBus, LoadData loadData, SaveData saveData)
        {
            _signalBus = signalBus;
            _loadData = loadData;
            _saveData = saveData;

            _signalBus.Subscribe<EnemyKilledSignal>(EnemyKilled);
            _signalBus.Subscribe<GameWonSignal>(GameEnded); 
            _signalBus.Subscribe<GameLostSignal>(GameEnded);
        }

        private void EnemyKilled(EnemyKilledSignal signal)
        {
            ++count;
        }

        private void GameEnded()
        {
            var data = _loadData.GetPlayerData<PlayerData>("PlayerData");
            data.coins += count;
            _saveData.Save(data, "PlayerData");
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameWonSignal>(GameEnded);
            _signalBus.Unsubscribe<GameLostSignal>(GameEnded);
            _signalBus.Unsubscribe<EnemyKilledSignal>(EnemyKilled);
        }
    }
}