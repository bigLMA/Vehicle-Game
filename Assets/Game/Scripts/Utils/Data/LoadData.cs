using System.IO;
using UnityEngine;

namespace VehicleGame.Utils.Data
{
    public class LoadData
    {
        public T Load<T>(string path) where T : new()
        {
            path = string.Concat(Application.persistentDataPath + "/" + path + ".json");

            if (File.Exists(path))
            {
                var json = File.ReadAllText(path);
                var data = JsonUtility.FromJson<T>(json);

                return data;
            }

            return new T();
        }
    }
}