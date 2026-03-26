using UnityEngine;

namespace VehicleGame.Core.Data.Configs
{
    [CreateAssetMenu(fileName = "TurretConfig", menuName = "Configs/Turret")]
    public class TurretConfig : ScriptableObject
    {
        [Header("Rotation")]
        public float sensitivity = 0.2f;
        public float damping = 5f;
        public float maxRotationSpeed = 200f;

        [Header("Shooting")]
        public float initialShootDelay = 1.75f;
        public float shootInterval = 0.75f;
        public float targetLineLength = 13.8f;
    }
}
