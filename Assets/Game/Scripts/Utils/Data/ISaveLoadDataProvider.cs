namespace VehicleGame.Utils.Data
{
    public interface ISaveLoadDataProvider
    {
        public string GetPlayerDataFileName();

        public string GetVehicleDataFileName();
    }
}