using System;
using UnityEngine;
using VehicleGame.Core.Interfaces;

namespace VehicleGame.Core.Gameplay.Enemy
{
    public class Enemy : MonoBehaviour
    {
        protected IHealth _health;

        public Action<Enemy> OnEnemyDeath;

        protected virtual void Awake()
        {
            _health = GetComponent<IHealth>();
            _health.OnDeath += OnDeath;
        }

        public virtual void Construct(Vector3 pos)
        {
            transform.position = pos;
        }

        protected virtual void OnDestroy()
        {
            _health.OnDeath -= OnDeath;
        }

        protected virtual void OnDeath()
        {
            OnEnemyDeath?.Invoke(this);
        }
    }
}

