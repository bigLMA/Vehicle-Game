
namespace VehicleGame.Core.Gameplay.Vehicle
{
    public class TurretUpgrade : VehicleUpgrader
    {
        public override string GetKey() => "turret";

        public override void Upgrade(VehicleUpgradeData data)
        {
            data.turretUpgrades += 1;
        }
    }
}

