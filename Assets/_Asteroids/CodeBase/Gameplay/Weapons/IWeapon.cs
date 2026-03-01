namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public interface IWeapon
    {
        public IWeaponState State { get; }
        public bool CanShoot();
        public void Shoot(ShootIntent shotIntent);

        public void ReleaseShoot();
    }
}
