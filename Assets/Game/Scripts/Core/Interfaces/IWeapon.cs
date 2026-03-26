using UnityEngine;

namespace VehicleGame.Core.Interfaces
{
    public interface IWeapon
    {
        void Configure(float initialDelay, float shootInterval, Transform muzzlePoint, float lineLength);

        void Shoot();

        void StartShooting();
        void StopShooting();

        void ResetWeapon();
    }
}
