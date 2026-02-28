namespace _Asteroids.CodeBase.Entities.Starship
{
    public struct MoveIntent
    {
        public bool Thrust;
        public float Rotation;

        public MoveIntent(bool thrust, float rotation)
        {
            Thrust = thrust;
            Rotation = rotation;
        }
    }
}
