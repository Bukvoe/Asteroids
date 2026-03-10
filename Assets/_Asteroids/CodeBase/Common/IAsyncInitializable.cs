using Cysharp.Threading.Tasks;

namespace _Asteroids.CodeBase.Common
{
    public interface IAsyncInitializable
    {
        UniTask InitializeAsync();
    }
}
