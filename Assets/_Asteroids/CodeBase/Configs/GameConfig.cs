using System.Collections.Generic;

namespace _Asteroids.CodeBase.Configs
{

    public class GameConfig
    {
        public StarshipConfig Starship;

        public AsteroidSpawnConfig AsteroidSpawn;
        public List<AsteroidConfig> Asteroids;

        public EnemySpawnConfig EnemySpawn;
        public UfoConfig Ufo;

        public ScoreConfig Score;
    }
}
