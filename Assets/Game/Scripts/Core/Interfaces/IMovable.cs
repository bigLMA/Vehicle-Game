using UnityEngine;

namespace VehicleGame.Core.Interfaces
{
    public interface IMovable
    {
        void SetSpeed(float speed);

        void SetDirection(Vector3 direction);

        Transform GetTransform();

        void StartMoving();
        void Stop();
    }
}
