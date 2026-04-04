using System.IO;
using UnityEngine;

namespace VehicleGame.Utils.PlayerData
{
    // TODO register
    public class SavePlayerData
    {
        public void Save(VehicleGame.Core.PlayerStats.PlayerData data)
        {
            var path = $"{Application.persistentDataPath}/PlayerData.json";
            var json = JsonUtility.ToJson(data);

            File.WriteAllText(path, json);
        }
    }
}