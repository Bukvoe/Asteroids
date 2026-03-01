using System.Collections.Generic;
using UnityEngine;

namespace _Asteroids.CodeBase.Configs
{
    public class GameConfig : MonoBehaviour
    {
        [field: SerializeField] public List<AsteroidConfig> Asteroids { get; private set; }
    }
}
