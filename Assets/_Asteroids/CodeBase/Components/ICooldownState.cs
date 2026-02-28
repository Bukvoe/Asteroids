namespace _Asteroids.CodeBase.Components
{
    public interface ICooldownState
    {
        public const float CooldownUpdateInterval = 0.1f;

        public float CurrentCooldown { get; }
    }
}
