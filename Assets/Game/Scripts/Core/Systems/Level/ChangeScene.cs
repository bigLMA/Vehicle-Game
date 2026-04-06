using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using Zenject;

namespace VehicleGame.Core.Systems.Level
{
    public class ChangeScene
    {
        public float progress { get; private set; }
        private ZenjectSceneLoader _sceneLoader;

        [Inject]
        public ChangeScene(ZenjectSceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void LoadScene(string sceneName)
        {
            _sceneLoader.LoadScene(sceneName);
        }
    }
}

