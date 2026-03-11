using _Asteroids.CodeBase.Factories;
using _Asteroids.CodeBase.Gameplay.Starship;

namespace _Asteroids.CodeBase.Services
{
    public class StarshipService
    {
        private readonly StarshipFactory _starshipFactory;

        public Starship Starship { get; private set; }


        public StarshipService(StarshipFactory starshipFactory)
        {
            _starshipFactory = starshipFactory;
        }

        public void CreateStarship()
        {
            Starship = _starshipFactory.Create();
        }
    }
}
