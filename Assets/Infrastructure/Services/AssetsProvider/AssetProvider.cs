using Infrastructure.Services.PrefabLoad;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.AssetsProvider
{
    public class AssetProvider : IAssetProvider
    {
        private readonly IPrefabLoader _prefabLoader;
        private readonly DiContainer _container;

        public AssetProvider(IPrefabLoader prefabLoader, DiContainer container)
        {
            _prefabLoader = prefabLoader;
            _container = container;
        }
        
        public TType Instantiate<TType>(string path) where TType : Object => 
            Object.Instantiate(_prefabLoader.LoadPrefab<TType>(path));
        
        public TType Instantiate<TType>(string path, Transform parent) where TType : Object => 
            Object.Instantiate(_prefabLoader.LoadPrefab<TType>(path), parent);

        public TType InstantiateFromZenject<TType>(string path, Transform parent) where TType : Object  =>
            _container.InstantiatePrefabForComponent<TType>(_prefabLoader.LoadPrefab<TType>(path), parent);

        public GameObject Instantiate(string path) =>  
            (GameObject) Object.Instantiate(_prefabLoader.LoadPrefab(path));
        
        public GameObject Instantiate(string path, Transform parent) =>
            (GameObject) Object.Instantiate(_prefabLoader.LoadPrefab(path), parent);

        
    }
}