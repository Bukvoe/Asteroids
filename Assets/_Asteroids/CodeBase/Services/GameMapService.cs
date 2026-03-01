using UnityEngine;

namespace _Asteroids.CodeBase.Services
{
    public class GameMapService
    {
        public readonly float Width;
        public readonly float Height;
        private readonly RandomService _randomService;

        public GameMapService(SceneObjectService sceneObjectService, RandomService randomService)
        {
            var size = sceneObjectService.Camera.ViewportToWorldPoint(Vector3.one);

            Width = size.x;
            Height = size.y;

            _randomService = randomService;
        }

        public Vector3 WrapPosition(Vector3 position, float objectRadius)
        {
            position.x = WrapAxis(position.x, Width, objectRadius);
            position.y = WrapAxis(position.y, Height, objectRadius);

            return position;
        }

        public Vector2 GetMapRandomPoint()
        {
            var x = Random.Range(-Width, Width);
            var y = Random.Range(-Height, Height);

            return new Vector2(x, y);
        }

        public Vector2 GetSpawnRandomPoint()
        {
            const float offset = 2f;

            var isVertical = _randomService.RandomBool();

            var x = isVertical
                ? Random.Range(-Width, Width)
                : _randomService.RandomBool() ? -Width - offset : Width + offset;

            var y = isVertical
                ? _randomService.RandomBool() ? -Height - offset : Height + offset
                : Random.Range(-Height, Height);

            return new Vector2(x, y);
        }

        public bool IsInsideMap(Vector3 position, float objectRadius)
        {
            return position.x >= -Width + objectRadius &&
                   position.x <= Width - objectRadius &&
                   position.y >= -Height + objectRadius &&
                   position.y <= Height - objectRadius;
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
