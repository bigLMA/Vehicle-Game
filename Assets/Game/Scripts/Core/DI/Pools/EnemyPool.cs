using UnityEngine;
using VehicleGame.Core.Gameplay.Enemy;
using Zenject;

namespace VehicleGame.Core.DI
{
    public class EnemyPool : MonoMemoryPool<Vector3, Enemy>
    {
        protected override void Reinitialize(Vector3 pos, Enemy enemy)
        {
            enemy.Construct(pos);
        }

        protected override void OnDespawned(Enemy enemy)
        {
            base.OnDespawned(enemy);
        }
    }
}
