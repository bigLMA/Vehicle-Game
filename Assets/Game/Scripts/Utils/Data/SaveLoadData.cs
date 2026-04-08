namespace VehicleGame.Utils.Data
{
    public class SaveLoadData : ISaveLoadDataProvider
    {
        private readonly string playerDataFileName = "PlayerData";
        private readonly string vehicleDataFileName = "VehicleData";

        public string GetPlayerDataFileName() => playerDataFileName;

        public string GetVehicleDataFileName()=> vehicleDataFileName;
    }
}