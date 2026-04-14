using System;
using UnityEngine;
using VehicleGame.Core.Gameplay.Vehicle;


namespace VehicleGame.Core.Gameplay.UpgradeSelector
{
    public class UpgradeSelectorSlot : MonoBehaviour
    {
        public event Action<VehicleUpgrader> OnClick;

        public VehicleUpgrader _upgrader { get; private set; }

        public void InsertSlot(VehicleUpgrader upgrade)
        {
            _upgrader = upgrade;
            _upgrader.transform.parent = transform;
            _upgrader.transform.position = transform.position - new Vector3(0f, 0f, 0.5f);
            _upgrader.transform.rotation = Quaternion.Euler(0f, 90f, 0f);
            _upgrader.OnUpgrade += OnUpgrade;
        }

        private void OnUpgrade(bool success)
        {
            if (success)
            {
                OnClick?.Invoke(_upgrader);
                _upgrader.OnUpgrade -= OnUpgrade;
                Destroy(_upgrader.gameObject);
                _upgrader = null;
            }
            else
            {
                _upgrader.transform.position = transform.position - new Vector3(0f, 0f, 0.5f);
            }
        }

        private void OnDestroy()
        {
            if (_upgrader != null)
            {
                _upgrader.OnUpgrade -= OnUpgrade;
                _upgrader = null;
            }
        }
    }
}
