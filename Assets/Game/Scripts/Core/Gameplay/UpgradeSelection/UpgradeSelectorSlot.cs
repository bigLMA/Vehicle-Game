using System;
using UnityEngine;
using VehicleGame.Core.Gameplay.Vehicle;


namespace VehicleGame.Core.Gameplay.UpgradeSelector
{
    public class UpgradeSelectorSlot : MonoBehaviour
    {
        public event Action<VehicleUpgrader> OnClick;

        public VehicleUpgrader _upgrader { get; private set; }

        private void OnMouseDown()
        {
            if (_upgrader == null) return;

            OnClick?.Invoke(_upgrader);
            Destroy(_upgrader.gameObject);
            _upgrader = null;
        }

        public void InsertSlot(VehicleUpgrader upgrade)
        {
            _upgrader = Instantiate(upgrade, transform.position - new Vector3(0f,0f, 0.5f), Quaternion.Euler(0f, 90f, 0f));
        }
    }
}
