using _Asteroids.CodeBase.Factories.Payloads;
using Zenject;

namespace _Asteroids.CodeBase.Components
{
    public partial class BulletWeapon
    {
        public class Factory : PlaceholderFactory<BulletWeaponSpawnPayload, BulletWeapon>
        {
        }
    }
}
