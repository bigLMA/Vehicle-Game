using UnityEngine;
using System.Collections.Generic;
using VehicleGame.Core.Gameplay.Vehicle;
using Zenject;
using System.Linq;
using VehicleGame.Core.Events;

namespace VehicleGame.Core.Gameplay.UpgradeSelector
{
    public class UpgradeSelector : MonoBehaviour
    {
        [SerializeField]
        private UpgradeSelectorSlot[] _slots;

        [SerializeField]
        private VehicleUpgrader[] _upgraders;

        private Dictionary<string, VehicleUpgrader> _upgradeDictionary;

        private VehicleUpgradeViewModel _vehicleUpgradeViewModel;
        private SignalBus _signalBus;

        [Inject]
        private void Initialize(VehicleUpgradeViewModel vehicleUpgradeViewModel, SignalBus signalBus)
        {
            _signalBus = signalBus;
            _vehicleUpgradeViewModel = vehicleUpgradeViewModel;
        }

        private void Awake()
        {
            _upgradeDictionary = new();

            foreach(var upgrade in _upgraders)
            {
                _upgradeDictionary.Add(upgrade.GetKey(), upgrade);
            }

            foreach(var slot in _slots)
            {
                slot.OnClick += Slot_OnClick;
            }

            _signalBus.Subscribe<PurchasedSignal>(AddUpgrade);
        }

        public void AddUpgrade()
        {
            UpgradeSelectorSlot emptySlot = null;

            foreach (var slot in _slots)
            {
                if(slot._upgrader==null)
                { 
                    emptySlot = slot;
                    break;
                }
            }

            // Get Random key
            var keys = _upgradeDictionary.Keys.ToArray();
            var idx = Random.Range(0, keys.Length);
            var key = keys[idx];

            // Insert slot
            emptySlot.InsertSlot(_upgradeDictionary[key]);
        }

        private void Slot_OnClick(VehicleUpgrader upgrade)
        {
            if (upgrade == null) return;

            _vehicleUpgradeViewModel.Upgrade(upgrade);
        }

        private void OnDestroy()
        {
            foreach (var slot in _slots)
            {
                slot.OnClick -= Slot_OnClick;
            }
        }
    }
}
