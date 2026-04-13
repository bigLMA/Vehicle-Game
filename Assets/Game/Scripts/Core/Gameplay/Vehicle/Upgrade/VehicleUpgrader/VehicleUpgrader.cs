
using UnityEngine;

namespace VehicleGame.Core.Gameplay.Vehicle
{
    public abstract class VehicleUpgrader : MonoBehaviour
    {
        public abstract string GetKey(); 

        public abstract void Upgrade(VehicleUpgradeData data);
    }
}

