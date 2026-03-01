using System;
using _Asteroids.CodeBase.Gameplay.Starship;
using _Asteroids.CodeBase.Services;
using Zenject;

namespace _Asteroids.CodeBase.UI
{
    public class LosePresenter : IInitializable, IDisposable
    {
        private readonly LoseView _view;
        private readonly Starship _starship;
        private readonly GameSessionService _gameSessionService;

        public LosePresenter(LoseView view, Starship starship, GameSessionService gameSessionService)
        {
            _view = view;
            _starship = starship;
            _gameSessionService = gameSessionService;
        }

        public void Initialize()
        {
            _starship.OnDestroyed += OnStarshipDestroyed;

            _view.Hide();
        }

        public void Dispose()
        {
            if (_starship != null)
            {
                _starship.OnDestroyed -= OnStarshipDestroyed;
            }
        }

        private void OnStarshipDestroyed()
        {
            _view.UpdateScore(_gameSessionService.Score);
            _view.Show();
        }
    }
}
