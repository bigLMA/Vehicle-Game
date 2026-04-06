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

        public async void ChangeSceneAsync(string sceneName)
        {
            var operation = _sceneLoader.LoadSceneAsync(sceneName);
            operation.allowSceneActivation = false;
            progress = operation.progress;

            while(!operation.isDone)
            {
                await UniTask.Yield();

                if(operation.progress>0.9f && operation.allowSceneActivation ==false)
                {
                    operation.allowSceneActivation=true;
                }

                progress = operation.progress;
            }

            progress = 0f;
        }
    }
}

