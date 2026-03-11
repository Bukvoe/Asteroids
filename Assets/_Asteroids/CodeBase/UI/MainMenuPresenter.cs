using System;
using _Asteroids.CodeBase.Services.SceneLoad;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Asteroids.CodeBase.UI
{
    public class MainMenuPresenter : IInitializable, IDisposable
    {
        private readonly MainMenuView _view;
        private readonly ISceneLoadService _sceneLoadService;

        public MainMenuPresenter(MainMenuView view, ISceneLoadService sceneLoadService)
        {
            _view = view;
            _sceneLoadService = sceneLoadService;
        }

        public void Initialize()
        {
            _view.PlayRequested += OnPlayRequested;
            _view.QuitRequested += OnQuitRequested;
        }

        public void Dispose()
        {
            _view.PlayRequested -= OnPlayRequested;
            _view.QuitRequested -= OnQuitRequested;
        }

        private void OnPlayRequested()
        {
            _sceneLoadService.LoadSceneAsync(GameScene.Gameplay).Forget();
        }

        private void OnQuitRequested()
        {
            Application.Quit();
        }
    }
}
