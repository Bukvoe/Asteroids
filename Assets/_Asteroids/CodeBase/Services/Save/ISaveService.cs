using _Asteroids.CodeBase.Data;

namespace _Asteroids.CodeBase.Services.Save
{
    public interface ISaveService
    {
        void Save(PlayerProgress progress);
        PlayerProgress Load();
    }
}
