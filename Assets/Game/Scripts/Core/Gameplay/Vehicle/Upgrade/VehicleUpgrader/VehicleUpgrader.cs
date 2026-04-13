
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

namespace VehicleGame.Core.Gameplay.Vehicle
{
    public abstract class VehicleUpgrader : MonoBehaviour
    {
        private Vector3 _offset;

        public abstract string GetKey(); 

        public abstract void Upgrade(VehicleUpgradeData data);

        private void OnMouseDrag()
        {
            var zDepth = Camera.main.WorldToScreenPoint(transform.position).z;

            Vector3 touchWorld = Camera.main.ScreenToWorldPoint(
                new Vector3(Input.mousePosition.x,
                Input.mousePosition.y,
                zDepth));

            touchWorld.z = transform.position.z;

            transform.position = touchWorld;
        }

        private void OnMouseUp()
        {
            
        }
    }
}

