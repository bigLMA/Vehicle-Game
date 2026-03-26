using UnityEngine;

namespace VehicleGame.Core.Data.Configs
{
    [CreateAssetMenu(fileName = "VehicleConfig", menuName = "Configs/Vehicle")]
    public class VehicleConfig : ScriptableObject
    {
        public float vehicleSpeed;
        public int vehicleHealthPoints;
    }
}
