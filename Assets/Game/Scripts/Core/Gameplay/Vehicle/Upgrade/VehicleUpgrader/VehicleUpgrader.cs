using System;
using UnityEngine;
using VehicleGame.Core.Events;
using VehicleGame.Core.Systems.GameManager;
using Zenject;

namespace VehicleGame.Core.Gameplay.Vehicle
{
    public abstract class VehicleUpgrader : MonoBehaviour
    {
        private Vector3 _offset;

        [Inject]
        private SignalBus _signalBus;

        public event Action<bool> OnUpgrade;

        public abstract string GetKey(); 

        public abstract void Upgrade(VehicleUpgradeData data);

        private void Start()
        {
            var collider = GetComponent<Collider>();
            collider.enabled = false;
            collider.enabled = true;
        }

        private void OnMouseDrag()
        {
            print("DRAG");

            var zDepth = Camera.main.WorldToScreenPoint(transform.position).z;

            Vector3 touchWorld = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x,
                Input.mousePosition.y,
                zDepth));

            touchWorld.z = transform.position.z;

            transform.position = touchWorld;

            _signalBus.Fire(new DragUpdateStartedSignal(GetKey()));
        }

        private void OnMouseUp()
        {
            print("DRAG Ended");
            var signal = new DragUpdateEndedSignal(transform.position);
            _signalBus.Fire(signal);

            OnUpgrade?.Invoke(signal.success);
        }
    }
}

