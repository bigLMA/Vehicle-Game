using System;
using UnityEngine;


namespace VehicleGame.Core.Gameplay.UpgradeSelector
{
    public class UpgradeSelectorSlot : MonoBehaviour
    {
        [SerializeField]
        private int _slotIndex;

        public event Action<int> OnClick;

        private void OnMouseDown()
        {
            OnClick?.Invoke(_slotIndex);
        }
    }
}
