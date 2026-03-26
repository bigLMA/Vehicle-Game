using UnityEngine;

namespace VehicleGame.Core.Interfaces
{
    public interface IVehicle : IDamageable
    {
        public Transform GetTransform();

        public void ResetVehicle();
    }
}