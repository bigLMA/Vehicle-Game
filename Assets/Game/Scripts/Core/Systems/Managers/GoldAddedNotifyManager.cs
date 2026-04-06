using System;
using System.Collections.Generic;
using UnityEngine;
using VehicleGame.Core.DI;
using VehicleGame.Core.Events;
using VehicleGame.Core.UI.FloatingUI;
using Zenject;

namespace VehicleGame.Core.Systems.Managers
{
    public class GoldAddedNotifyManager : IDisposable
    {
        private List<FloatingUI> _notifiersList = new();
        AddCoinsNotifierPool _notifiersPool; 
        private SignalBus _signalBus;

        private Vector3 _spawnOffset = new Vector3(0f, 0.5f);

        [Inject]
        private void Initialize(AddCoinsNotifierPool notifiersPool, SignalBus signalBus)
        {
            _notifiersPool = notifiersPool;
            _signalBus = signalBus;

            _signalBus.Subscribe<EnemyKilledSignal>(EnemyKilled);
            _signalBus.Subscribe<GameLostSignal>(CleanNotifiers);
            _signalBus.Subscribe<GameWonSignal>(CleanNotifiers);
        }

        private void EnemyKilled(EnemyKilledSignal signal)
        {
            var notifier = _notifiersPool.Spawn(signal.deathCoordinate+_spawnOffset);
            _notifiersList.Add(notifier);
            notifier.OnTimerOver += Notifier_OnTimerOver;
        }

        private void Notifier_OnTimerOver(FloatingUI target)
        {
            target.OnTimerOver -= Notifier_OnTimerOver;
            _notifiersList.Remove(target);
            _notifiersPool.Despawn(target);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<EnemyKilledSignal>(EnemyKilled);
            _signalBus.Unsubscribe<GameLostSignal>(CleanNotifiers);
            _signalBus.Unsubscribe<GameWonSignal>(CleanNotifiers);

            CleanNotifiers();

            _notifiersList.Clear();
        }

        private void CleanNotifiers()
        {
            foreach (var notifier in _notifiersList)
            {
                notifier.OnTimerOver -= Notifier_OnTimerOver;
               // _notifiersPool.Despawn(notifier);
            }
        }
    }
}

