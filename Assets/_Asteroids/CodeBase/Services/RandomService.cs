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

        public float RandomAngle()
        {
            return Random.Range(0f, 360f);
        }

        private Vector2 RandomDirection()
        {
            var angle = RandomAngle();
            return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));
        }
    }
}
