using _Asteroids.CodeBase.Factories.Payloads;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public partial class LaserWeapon
    {
        public class Factory : PlaceholderFactory<LaserWeaponSpawnPayload, LaserWeapon>
        {
        }
    }
}
