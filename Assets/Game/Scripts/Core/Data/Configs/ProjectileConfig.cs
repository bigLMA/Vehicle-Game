using UnityEngine;

namespace VehicleGame.Core.Data.Configs
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "Configs/Projectile")]
    public class ProjectileConfig : ScriptableObject
    {
        public float speed = 40f;
        public float lifeTimer = 0.5f;
        public int damage = 5;
    }
}