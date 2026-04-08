
namespace VehicleGame.Core.Gameplay.Vehicle
{
    public class WheelUpgrade : VehicleUpgrader
    {
        public override void Upgrade(VehicleUpgradeData data)
        {
            data.wheelUpgrades += 1;
        }
    }
}

