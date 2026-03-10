using _Asteroids.CodeBase.Common;
using Cysharp.Threading.Tasks;

namespace _Asteroids.CodeBase.Services.Asset
{
    public interface IAssetService : IAsyncInitializable
    {
        public UniTask<T> LoadAsync<T>(string key) where T : class;

        public void Release(string key);

        public void ReleaseAll();
    }
}
