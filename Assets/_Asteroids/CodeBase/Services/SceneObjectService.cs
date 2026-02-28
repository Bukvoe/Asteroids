using UnityEngine;

namespace _Asteroids.CodeBase.Services
{
    public class SceneObjectService
    {
        public SceneObjectService(Camera camera)
        {
            Camera = camera;
        }

        public Camera Camera { get; private set; }
    }
}
