using TMPro;
using UnityEngine;
using VehicleGame.Core.UI.Buttons;
using VehicleGame.Core.UI.HUD;
using Zenject;

namespace VehicleGame.Core.UI.Buttons
{
    public class PurchaseButton : ButtonBase
    {
        private TextMeshProUGUI _buttonTextField;
        private CoinsViewModel _coinsViewModel;

        [Inject]
        private void Initialize(CoinsViewModel coinsViewModel)
        {
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
            //if(_coinsViewModel.CheckCoinPurchase())
            //{
            //    _coinsViewModel.Purchase();
            //}
        }

        private void UpdateText()
        {
            _buttonTextField.text = $"Purchase ({0})";
        }
    }
}

