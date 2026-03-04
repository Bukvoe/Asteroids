using System;
using _Asteroids.CodeBase.Services;
using _Asteroids.CodeBase.Services.SceneLoad;
using Zenject;

namespace _Asteroids.CodeBase.UI
{
    public class LosePresenter : IInitializable, IDisposable
    {
        private readonly LoseView _view;
        private readonly CurrentRunService _currentRunService;
        private readonly ISceneLoadService _sceneLoadService;
        private readonly PlayerProgressService _playerProgressService;

        public LosePresenter(
            LoseView view,
            CurrentRunService currentRunService,
            ISceneLoadService sceneLoadService,
            PlayerProgressService playerProgressService)
        {
            _view = view;
            _currentRunService = currentRunService;
            _sceneLoadService = sceneLoadService;
            _playerProgressService = playerProgressService;
        }

        public void Initialize()
        {
            _currentRunService.RunEnded += OnRunEnded;
            _view.RestartRequested += OnRestartRequested;

            _view.Hide();
        }

        public void Dispose()
        {
            _currentRunService.RunEnded -= OnRunEnded;
            _view.RestartRequested -= OnRestartRequested;
        }

        private void OnRunEnded()
        {
            _view.UpdateScore(_currentRunService.Score);

            _view.UpdateBestScore(_playerProgressService.BestScore);
            _view.UpdateRuns(_playerProgressService.Runs);
            _view.UpdateUfoDestroyed(_playerProgressService.UfoDestroyed);

            _view.Show();
        }

        private void OnRestartRequested()
        {
            _sceneLoadService.ReloadCurrentScene();
        }
    }
}
