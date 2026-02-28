using UnityEngine;

namespace _Asteroids.CodeBase.Services
{
    public class GameMapService
    {
        public readonly float Width;
        public readonly float Height;

        public GameMapService(SceneObjectService sceneObjectService)
        {
            var size = sceneObjectService.Camera.ViewportToWorldPoint(Vector3.one);

            Width = size.x;
            Height = size.y;
        }

        public Vector3 WrapPosition(Vector3 position, float objectRadius)
        {
            position.x = WrapAxis(position.x, Width, objectRadius);
            position.y = WrapAxis(position.y, Height, objectRadius);

            return position;
        }

        private float WrapAxis(float value, float boundary, float offset)
        {
            if (Mathf.Abs(value) > boundary + offset)
            {
                return -Mathf.Sign(value) * boundary;
            }

            return value;
        }
    }
}
