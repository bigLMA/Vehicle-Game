using UnityEngine;

namespace VehicleGame.Core.Move
{
    public class MoveRigidBody : MoveComponent
    {
        private Rigidbody _rb;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (_isMoving)
            {
                _rb.linearVelocity = Vector3.zero;
                _rb.MovePosition(_rb.position + _direction * _speed * Time.deltaTime);
            }
        }
    }
}

