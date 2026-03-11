using System.Collections.Generic;
using _Asteroids.CodeBase.Common;
using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace _Asteroids.CodeBase.Services.Asset
{
    public class AddressablesAssetService : IAssetService, IAsyncInitializable
    {
        private readonly Dictionary<string, AsyncOperationHandle> _cachedAssets = new();
        private readonly Dictionary<string, List<AsyncOperationHandle>> _handles = new();

        public async UniTask InitializeAsync()
        {
            await Addressables.InitializeAsync();
        }

        public async UniTask<T> LoadAsync<T>(string key) where T : class
        {
            if (_cachedAssets.TryGetValue(key, out var completedHandle))
            {
                return completedHandle.Result as T;
            }

            var handle = Addressables.LoadAssetAsync<T>(key);


            handle.Completed += OnAssetLoaded;

            if (!_handles.TryGetValue(key, out var handles))
            {
                handles = new List<AsyncOperationHandle>();
                _handles[key] = handles;
            }

            handles.Add(handle);

            return await handle.Task;

            void OnAssetLoaded(AsyncOperationHandle<T> completeHandle)
            {
                if (_cachedAssets.ContainsKey(key))
                {
                    _cachedAssets[key] = completeHandle;
                }

                handle.Completed -= OnAssetLoaded;
            }
        }

        public void Release(string key)
        {
            if (_cachedAssets.TryGetValue(key, out var cachedHandle))
            {
                cachedHandle.Release();
                _cachedAssets.Remove(key);
            }

            if (!_handles.TryGetValue(key, out var list))
            {
                return;
            }

            foreach (var handle in list)
            {
                handle.Release();
            }

            _handles.Remove(key);
        }

        public void ReleaseAll()
        {
            foreach (var keyHandlesPair in _handles)
            {
                foreach (var handle in keyHandlesPair.Value)
                {
                    handle.Release();
                }
            }

            _cachedAssets.Clear();
            _handles.Clear();
        }
    }
}
