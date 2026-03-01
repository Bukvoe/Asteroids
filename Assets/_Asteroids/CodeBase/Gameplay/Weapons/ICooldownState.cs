namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public interface ICooldownState
    {
        public const float CooldownUpdateInterval = 0.1f;

        public float CurrentCooldown { get; }
    }
}
