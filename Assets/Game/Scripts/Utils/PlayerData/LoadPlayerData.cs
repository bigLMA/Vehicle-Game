using System.IO;
using UnityEngine;

namespace VehicleGame.Utils.PlayerData
{
    // TODO register
    public class LoadPlayerData
    {
        public VehicleGame.Core.PlayerStats.PlayerData GetPlayerData()
        {
            var path = $"{Application.persistentDataPath}/PlayerData.json";

            if(File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<VehicleGame.Core.PlayerStats.PlayerData>(json);

                return data;
            }

            return new Core.PlayerStats.PlayerData();
        }
    }
}