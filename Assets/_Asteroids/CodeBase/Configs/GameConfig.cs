using System.Collections.Generic;
using UnityEngine;

namespace _Asteroids.CodeBase.Configs
{
    public class GameConfig : MonoBehaviour
    {
        [field: SerializeField] public StarshipConfig Starship { get; private set; }

        [field: SerializeField] public AsteroidSpawnConfig AsteroidSpawn { get; private set; }
        [field: SerializeField] public List<AsteroidConfig> Asteroids { get; private set; }

        [field: SerializeField] public ScoreConfig Score { get; private set; }
    }
}
