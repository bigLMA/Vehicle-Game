using UnityEngine;
using UnityEngine.UI;

namespace VehicleGame.Core.UI.Buttons
{
    public abstract class ButtonBase : MonoBehaviour
    {
        protected Button _button;

        void Start()
        {
            _button = GetComponent<Button>();

            _button.onClick.AddListener(() =>
            {
                ButtonClickListener();
            });
        }

        public abstract void ButtonClickListener();

        private void OnDestroy()
        {
            _button.onClick.RemoveAllListeners();
        }
    }
}

