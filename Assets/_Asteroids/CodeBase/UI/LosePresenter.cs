using System;
using _Asteroids.CodeBase.Gameplay.Starship;
using _Asteroids.CodeBase.Services;
using _Asteroids.CodeBase.Services.SceneLoad;
using Zenject;

namespace _Asteroids.CodeBase.UI
{
    public class LosePresenter : IInitializable, IDisposable
    {
        private readonly LoseView _view;
        private readonly Starship _starship;
        private readonly GameSessionService _gameSessionService;
        private readonly ISceneLoadService _sceneLoadService;

        public LosePresenter(
            LoseView view,
            Starship starship,
            GameSessionService gameSessionService,
            ISceneLoadService sceneLoadService)
        {
            _view = view;
            _starship = starship;
            _gameSessionService = gameSessionService;
            _sceneLoadService = sceneLoadService;
        }

        public void Initialize()
        {
            _starship.OnDestroyed += OnStarshipDestroyed;

            _view.RestartRequested += OnRestartRequested;

            _view.Hide();
        }

        public void Dispose()
        {
            if (_starship != null)
            {
                _starship.OnDestroyed -= OnStarshipDestroyed;
            }

            _view.RestartRequested -= OnRestartRequested;
        }

        private void OnStarshipDestroyed()
        {
            _view.UpdateScore(_gameSessionService.Score);
            _view.Show();
        }

        private void OnRestartRequested()
        {
            _sceneLoadService.ReloadCurrentScene();
        }
    }
}
