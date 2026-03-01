using _Asteroids.CodeBase.Factories.Payloads;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public partial class Laser
    {
        public class Factory : PlaceholderFactory<LaserSpawnPayload, Laser>
        {
        }
    }
}
