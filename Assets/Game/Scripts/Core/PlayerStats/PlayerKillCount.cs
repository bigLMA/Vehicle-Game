using System;
using VehicleGame.Core.Events;
using VehicleGame.Utils.PlayerData;
using Zenject;

namespace VehicleGame.Core.PlayerStats
{
    // TODO CREATE
    public class PlayerKillCount : IDisposable
    {
        private int count = 0;

        private SignalBus _signalBus;
        private LoadPlayerData _loadPlayerData;
        private SavePlayerData _savePlayerData;

        [Inject]
        private void Initialize(SignalBus signalBus, LoadPlayerData loadPlayerData, SavePlayerData savePlayerData)
        {
            _signalBus = signalBus;
            _loadPlayerData = loadPlayerData;
            _savePlayerData = savePlayerData;

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
            var data = _loadPlayerData.GetPlayerData();
            data.coins += count;
            _savePlayerData.Save(data);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<GameWonSignal>(GameEnded);
            _signalBus.Unsubscribe<GameLostSignal>(GameEnded);
            _signalBus.Unsubscribe<EnemyKilledSignal>(EnemyKilled);
        }
    }
}