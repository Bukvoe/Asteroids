using UnityEngine;

namespace _Asteroids.CodeBase.Services
{
    public class RandomService
    {
        public bool RandomBool()
        {
            return Random.value < 0.5f;
        }
    }
}
