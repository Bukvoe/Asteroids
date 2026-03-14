using System.Collections.Generic;
using UnityEngine;

namespace _Asteroids.CodeBase.Configs
{
    [CreateAssetMenu(menuName = "Configs/GameConfig")]
    public class GameConfigScriptableObject : ScriptableObject
    {
        [field: SerializeField] public StarshipConfig Starship { get; private set; }

        [field: SerializeField] public AsteroidSpawnConfig AsteroidSpawn { get; private set; }
        [field: SerializeField] public List<AsteroidConfig> Asteroids { get; private set; }

        [field: SerializeField] public EnemySpawnConfig EnemySpawn { get; private set; }
        [field: SerializeField] public UfoConfig Ufo { get; private set; }

        [field: SerializeField] public ScoreConfig Score { get; private set; }
    }
}
