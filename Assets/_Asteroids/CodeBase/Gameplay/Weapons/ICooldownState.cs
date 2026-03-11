namespace _Asteroids.CodeBase.Gameplay.Weapons
{
    public interface ICooldownState
    {
        public const float COOLDOWN_UPDATE_INTERVAL = 0.1f;

        public float CurrentCooldown { get; }
    }
}
