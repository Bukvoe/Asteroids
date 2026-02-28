namespace _Asteroids.CodeBase.Components
{
    public interface IWeapon
    {
        public IWeaponState State { get; }
        public bool CanShoot();
        public void Shoot(ShootIntent shotIntent);
    }
}
