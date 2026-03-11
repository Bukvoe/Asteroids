using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace _Asteroids.CodeBase.Services.SceneLoad
{
    public class SceneLoadService : ISceneLoadService
    {
        private const string BOOTSTRAP_SCENE = "BootstrapScene";
        private const string MAIN_MENU_SCENE = "MainMenuScene";
        private const string GAMEPLAY_SCENE = "GameplayScene";

        private readonly HashSet<string> _loadingScenes = new();
        private readonly Dictionary<string, SceneInstance> _loadedAddressableScenes = new();

        public async UniTask LoadSceneAsync(GameScene scene)
        {
            var sceneName = scene switch
            {
                GameScene.Bootstrap => BOOTSTRAP_SCENE,
                GameScene.MainMenu  => MAIN_MENU_SCENE,
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

            await LoadSceneInternalAsync(sceneName);
        }

        public async UniTask ReloadCurrentSceneAsync()
        {
            await LoadSceneInternalAsync(SceneManager.GetActiveScene().name);
        }

        private async UniTask LoadSceneInternalAsync(string sceneName)
        {
            if (!_loadingScenes.Add(sceneName))
            {
                return;
            }

            if (_loadedAddressableScenes.TryGetValue(sceneName, out var sceneInstance))
            {
                await Addressables.UnloadSceneAsync(sceneInstance);

                _loadedAddressableScenes.Remove(sceneName);
            }

            if (IsSceneAddressable(sceneName))
            {
                await LoadAddressableSceneAsync(sceneName);
            }
            else
            {
                await LoadInBuildSceneAsync(sceneName);
            }

            _loadingScenes.Remove(sceneName);
        }

        private async UniTask LoadAddressableSceneAsync(string sceneName)
        {
            var handle = Addressables.LoadSceneAsync(sceneName);

            await handle.ToUniTask();

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                _loadedAddressableScenes[sceneName] = handle.Result;
            }
        }

        private async UniTask LoadInBuildSceneAsync(string sceneName)
        {
            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single).ToUniTask();
        }

        private static bool IsSceneAddressable(string sceneName)
        {
            return sceneName.Equals(GAMEPLAY_SCENE);
        }
    }
}
