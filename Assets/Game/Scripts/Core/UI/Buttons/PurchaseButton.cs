using TMPro;
using UnityEngine;
using VehicleGame.Core.Gameplay.Vehicle;
using VehicleGame.Core.UI.HUD;
using Zenject;

namespace VehicleGame.Core.UI.Buttons
{
    public class PurchaseButton : ButtonBase
    {
        private TextMeshProUGUI _buttonTextField;
        private CoinsViewModel _coinsViewModel;
        private VehicleUpgradeViewModel _vehicleUpgradeViewModel;

        [Inject]
        private void Initialize(CoinsViewModel coinsViewModel, VehicleUpgradeViewModel vehicleUpgradeViewModel)
        {
            _vehicleUpgradeViewModel = vehicleUpgradeViewModel;
            _coinsViewModel = coinsViewModel;
        }

        private void Awake()
        {
            _buttonTextField = GetComponent<TextMeshProUGUI>();
        }

        public override void ButtonClickListener()
        {
            MakePurchase();
            UpdateText();
        }

        private void MakePurchase()
        {
            var cost = _vehicleUpgradeViewModel.GetCurrentUpgradeCost();

            if (_coinsViewModel.CheckCoinPurchase(cost))
            {
                _coinsViewModel.Purchase(cost);
            }
        }

        private void UpdateText()
        {
            _buttonTextField.text = $"Purchase ({_vehicleUpgradeViewModel.GetCurrentUpgradeCost()})";
        }
    }
}

