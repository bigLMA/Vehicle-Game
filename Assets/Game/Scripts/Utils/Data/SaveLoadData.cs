namespace VehicleGame.Utils.Data
{
    public class SaveLoadData : ISaveLoadDataProvider
    {
        private readonly string playerDataFileName = "PlayerData";

        public string GetPlayerDataFileName() => playerDataFileName;
    }
}