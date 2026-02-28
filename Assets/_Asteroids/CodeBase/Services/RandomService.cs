using UnityEngine;

namespace _Asteroids.CodeBase.Services
{
    public class RandomService
    {
        public bool RandomBool()
        {
            return Random.value < 0.5f;
        }

        public float ApplyRandomSign(float value)
        {
            if (RandomBool())
            {
                return value * -1;
            }

            return value;
        }
    }
}
