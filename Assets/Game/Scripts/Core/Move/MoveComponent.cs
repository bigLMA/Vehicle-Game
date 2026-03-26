using UnityEngine;
using VehicleGame.Core.Interfaces;

namespace VehicleGame.Core.Move
{
    public class MoveComponent : MonoBehaviour, IMovable
    {
        protected float _speed;
        protected bool _isMoving = false;
        protected Vector3 _direction;

        public Transform GetTransform() => transform;

        public void SetDirection(Vector3 direction) => _direction = direction;

        public void SetSpeed(float speed) => _speed = speed;

        public void StartMoving() => _isMoving = true;

        public void Stop() => _isMoving = false;
    }
}