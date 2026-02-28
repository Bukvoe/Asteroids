namespace _Asteroids.CodeBase.Entities.Starship
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
