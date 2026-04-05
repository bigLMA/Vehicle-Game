using System.IO;
using UnityEngine;

namespace VehicleGame.Utils.Data
{
    public class LoadData
    {
        public T GetPlayerData<T>(string path)
        {
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