using _Asteroids.CodeBase.Factories.Payloads;
using Zenject;

namespace _Asteroids.CodeBase.Entities.Asteroid
{
    public partial class Asteroid
    {
        public class Factory : PlaceholderFactory<AsteroidSpawnPayload, Asteroid>
        {
        }
    }
}
