using UnityEngine;

namespace VehicleGame.Core.Gameplay.Vehicle
{
    [CreateAssetMenu(fileName = "VehicleUpgradeConfig", menuName ="Configs/VehicleUpgrade")]
    public class VehicleUpgradeConfig : ScriptableObject
    {
        public int initialPrice = 10;
        public int priceIncrease = 10;

        [Header("Vehicle")]
        public int healthPerCar = 10;
        public float speedPerWheel = 0.2f;

        [Header("Turret")]
        public int damagePerTurret = 10;
    }
}

