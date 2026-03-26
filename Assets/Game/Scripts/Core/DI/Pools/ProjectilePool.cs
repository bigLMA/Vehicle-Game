using UnityEngine;
using VehicleGame.Core.Gameplay.Projectile;
using Zenject;

namespace VehicleGame.Core.DI
{
    public class ProjectilePool : MonoMemoryPool<Vector3, Vector3, Projectile>
    {
        protected override void Reinitialize(Vector3 pos, Vector3 dir, Projectile item)
        {
            item.Construct(pos, dir, this);
        }
    }
}

