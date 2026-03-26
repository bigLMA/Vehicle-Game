using UnityEngine;

namespace VehicleGame.Core.Move
{
    public class MoveTransform : MoveComponent
    {
        public void Update()
        {
            if (_isMoving)
            {
                transform.LookAt(transform.position + _direction);
                transform.position += _direction * _speed * Time.deltaTime;
            }
        }
    }
}

