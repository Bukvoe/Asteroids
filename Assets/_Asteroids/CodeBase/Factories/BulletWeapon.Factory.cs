using _Asteroids.CodeBase.Factories.Payloads;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public partial class BulletWeapon
    {
        public class Factory : PlaceholderFactory<BulletWeaponSpawnPayload, BulletWeapon>
        {
        }
    }
}
