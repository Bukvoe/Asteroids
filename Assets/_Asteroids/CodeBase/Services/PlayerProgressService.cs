using _Asteroids.CodeBase.Data;
using _Asteroids.CodeBase.Services.Save;
using Zenject;

namespace _Asteroids.CodeBase.Services
{
    public class PlayerProgressService : IInitializable
    {
        private readonly ISaveService _saveService;
        private PlayerProgress _playerProgress;

        public int BestScore => _playerProgress.BestScore;

        public int Runs => _playerProgress.Runs;

        public int UfoDestroyed => _playerProgress.UfoDestroyed;

        public PlayerProgressService(ISaveService saveService)
        {
            _saveService = saveService;
        }

        public void Initialize()
        {
            _playerProgress = _saveService.Load();
        }

        public void UpdateProgress(RunResult runResult)
        {
            if (runResult.Score > BestScore)
            {
                _playerProgress.BestScore = runResult.Score;
            }

            _playerProgress.UfoDestroyed += runResult.UfoDestroyed;
            _playerProgress.Runs++;

            _saveService.Save(_playerProgress);
        }
    }
}
