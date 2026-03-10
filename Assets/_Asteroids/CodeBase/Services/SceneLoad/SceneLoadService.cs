using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Asteroids.CodeBase.Services.SceneLoad
{
    public class SceneLoadService : ISceneLoadService
    {
        private const string BOOTSTRAP_SCENE = "BootstrapScene";
        private const string GAMEPLAY_SCENE = "GameplayScene";

        public void LoadScene(GameScene scene)
        {
            var sceneName = scene switch
            {
                GameScene.Bootstrap => BOOTSTRAP_SCENE,
                GameScene.Gameplay  => GAMEPLAY_SCENE,
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
