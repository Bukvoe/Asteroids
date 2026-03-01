using _Asteroids.CodeBase.Factories.Payloads;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public partial class Bullet
    {
        public class Factory : PlaceholderFactory<BulletSpawnPayload, Bullet>
        {
        }
    }
}
