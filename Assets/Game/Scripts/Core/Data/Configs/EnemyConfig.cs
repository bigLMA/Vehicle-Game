using UnityEngine;

namespace VehicleGame.Core.Data.Configs
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Configs/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [Header("Wandering")]
        public float wanderSpeed = 1f;
        [Tooltip("Pause time between wandering")]
        public float pauseTime = 2.2f;
        public float initialWanderingDelay = 2.5f;

        public float wanderRadius = 3f;

        public float xBounds = 4f;

        [Header("Chase")]
        public float chaseSpeed = 1.8f;
        public float chaseDistance = 12f;

        [Header("Health")]
        public int maxHealth = 15;

        [Header("Damage")]
        public int damage = 7;

        [Header("Bounty")]
        public int bounty = 5;
    }
}