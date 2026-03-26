using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace VehicleGame.Core.Systems.Input
{
    public class InputService : IInitializable, IDisposable
    {
        public Action OnTap;

        public Action<bool> OnPress;

        private InputSystem_Actions _actions;

        public Vector2 _swipe => _actions.Player.SwipeValue.ReadValue<Vector2>();

        public void Initialize()
        {
            _actions = new();

            _actions.Player.Enable();
            _actions.Player.Tap.started += Tap_started;

            _actions.Player.Touch.started += Touch_started;
            _actions.Player.Touch.canceled += Touch_canceled;
        }
        private void Touch_started(InputAction.CallbackContext obj)
        {
            OnPress?.Invoke(true);
        }

        private void Touch_canceled(InputAction.CallbackContext obj)
        {
            OnPress?.Invoke(false);
        }

        private void Tap_started(InputAction.CallbackContext obj)
        {
            OnTap?.Invoke();
        }

        public void Dispose()
        {
            _actions.Player.Tap.started -= Tap_started;
        }
    }
}
