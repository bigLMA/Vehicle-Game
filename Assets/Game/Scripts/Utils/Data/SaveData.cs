using System.IO;
using UnityEngine;

namespace VehicleGame.Utils.Data
{
    // TODO register
    public class SaveData
    {
        public void Save<T>(T data, string path)
        {
            path = string.Concat(Application.persistentDataPath + "/" + path + ".json");
            //var path = $"{Application.persistentDataPath}/PlayerData.json";
            var json = JsonUtility.ToJson(data);

            File.WriteAllText(path, json);
        }
    }
}