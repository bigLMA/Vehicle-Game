using System.IO;
using UnityEngine;

namespace VehicleGame.Utils.Data
{
    // TODO register
    public class LoadData
    {
        public T GetPlayerData<T>(string path)
        {
            //var path = $"{Application.persistentDataPath}/PlayerData.json";
            path = string.Concat(Application.persistentDataPath + "/" + path + ".json");

            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);

                return data;
            }

            return default;
        }
    }
}