using System.IO;
using UnityEngine;

namespace VehicleGame.Utils.Data
{
    public class SaveData
    {
        public void Save<T>(T data, string path)
        {
            path = string.Concat(Application.persistentDataPath + "/" + path + ".json");
            var json = JsonUtility.ToJson(data);

            File.WriteAllText(path, json);
        }
    }
}