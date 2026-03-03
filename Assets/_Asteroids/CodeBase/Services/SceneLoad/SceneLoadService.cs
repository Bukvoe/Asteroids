using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Asteroids.CodeBase.Services.SceneLoad
{
    public class SceneLoadService : ISceneLoadService
    {
        private const string BootstrapScene = "BootstrapScene";
        private const string GameplayScene = "GameplayScene";

        public void LoadScene(GameScene scene)
        {
            var sceneName = scene switch
            {
                GameScene.Bootstrap => BootstrapScene,
                GameScene.Gameplay  => GameplayScene,
                _                   => string.Empty,
            };

            if (string.IsNullOrEmpty(sceneName))
            {
                Debug.LogWarning($"{nameof(SceneLoadService)}: Unknown scene {scene}");
                return;
            }

            if (SceneManager.GetActiveScene().name == sceneName)
            {
                return;
            }

            SceneManager.LoadScene(sceneName);
        }

        public void ReloadCurrentScene()
        {
            var currentScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentScene);
        }
    }
}
