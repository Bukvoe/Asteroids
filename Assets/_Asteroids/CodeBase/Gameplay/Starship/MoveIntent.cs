namespace _Asteroids.CodeBase.Gameplay.Starship
{
    public struct MoveIntent
    {
        public readonly bool Thrust;
        public readonly float Rotation;

        public MoveIntent(bool thrust, float rotation)
        {
            Thrust = thrust;
            Rotation = rotation;
        }
    }
}
