using UnityEngine;

namespace VehicleGame.Core.Gameplay.UpgradeSelector
{
    public class UpgradeSelector : MonoBehaviour
    {
        [SerializeField]
        private UpgradeSelectorSlot[] slots;

        private void Awake()
        {
            foreach(var slot in slots)
            {
                slot.OnClick += Slot_OnClick;
            }
        }

        public void AddUpgrade()
        {
            UpgradeSelectorSlot emptySlot = null;

            foreach (var slot in slots)
            {
                // Search for forst emptySlot
            }
        }

        private void Slot_OnClick(int index)
        {

        }

        private void OnDestroy()
        {
            foreach (var slot in slots)
            {
                slot.OnClick -= Slot_OnClick;
            }
        }
    }
}
