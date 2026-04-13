
namespace VehicleGame.Core.Gameplay.Vehicle
{
    public class FrameUpgrade : VehicleUpgrader
    {
        public override string GetKey() => "frame";

        public override void Upgrade(VehicleUpgradeData data)
        {
            data.frameUpgrades += 1;
        }
    }
}

