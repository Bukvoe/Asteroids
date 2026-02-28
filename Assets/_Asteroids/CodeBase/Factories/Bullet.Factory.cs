using _Asteroids.CodeBase.Factories.Payloads;
using Zenject;

namespace _Asteroids.CodeBase.Components
{
    public partial class Bullet
    {
        public class Factory : PlaceholderFactory<BulletSpawnPayload, Bullet>
        {
        }
    }
}
