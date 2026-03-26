using UnityEngine;
using VehicleGame.Core.Interfaces;
using Zenject;

namespace VehicleGame.Core.Systems.Camera
{
    public class FollowCamera : MonoBehaviour
    {
        [SerializeField]
        private Vector3 _vehicleOffset = new Vector3(0f, 7f, -6f);

        private IVehicle _vehicle;

        [Inject]
        public void Initialize(IVehicle vehicle)
        {
            _vehicle = vehicle;
        }

        private void LateUpdate()
        {
            if (_vehicle != null)
                transform.position = _vehicle.GetTransform().position + _vehicleOffset;
        }
    }
}

