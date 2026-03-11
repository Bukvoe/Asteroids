using Cysharp.Threading.Tasks;

namespace _Asteroids.CodeBase.Services.SceneLoad
{
    public interface ISceneLoadService
    {
        public UniTask LoadSceneAsync(GameScene scene);

        public UniTask ReloadCurrentSceneAsync();
    }
}
