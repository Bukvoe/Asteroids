using _Asteroids.CodeBase.Factories.Payloads;
using Zenject;

namespace _Asteroids.CodeBase.Gameplay.Ufo
{
    public partial class Ufo
    {
        public class Factory : PlaceholderFactory<UfoSpawnPayload, Ufo>
        {
        }
    }
}
