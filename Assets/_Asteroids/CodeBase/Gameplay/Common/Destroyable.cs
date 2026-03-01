using System;
using UnityEngine;

namespace _Asteroids.CodeBase.Gameplay.Common
{
    public class Destroyable : MonoBehaviour
    {
        public event Action OnDestroyed;

        public void DestroySelf()
        {
            OnDestroyed?.Invoke();

            Destroy(gameObject);
        }
    }
}
