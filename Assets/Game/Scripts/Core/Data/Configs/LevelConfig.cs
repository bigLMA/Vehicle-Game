using UnityEngine;

namespace VehicleGame.Core.Data.Configs
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/Level")]
    public class LevelConfig : ScriptableObject
    {
        // Start/finish info
        public Vector3 startPoint;
        public Vector3 endPoint;

        public float zSpawnStart;
        public float zSpawnEnd;

        public float roadWidth = 8f;

        // Enemies info
        public int enemiesAmount = 45;

        public GameObject groundPrefab;
        public float groundSpawnInterval = 75f;
    }
}


