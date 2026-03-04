using System;

namespace _Asteroids.CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public int BestScore;
        public int Runs;
        public int UfoDestroyed;

        public PlayerProgress(int bestScore, int runs, int ufoDestroyed)
        {
            BestScore = bestScore;
            Runs = runs;
            UfoDestroyed = ufoDestroyed;
        }
    }
}
