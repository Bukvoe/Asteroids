using System;
using _Asteroids.CodeBase.Gameplay.Starship;
using Zenject;

namespace _Asteroids.CodeBase.UI
{
    public class LosePresenter : IInitializable, IDisposable
    {
        private readonly LoseView _view;
        private readonly Starship _starship;

        public LosePresenter(LoseView view, Starship starship)
        {
            _view = view;
            _starship = starship;
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
            _view.Show();
        }
    }
}
