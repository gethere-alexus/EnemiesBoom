using Infrastructure.Services.PrefabLoad;
using UnityEngine;

namespace Infrastructure.Services.AssetsProvider
{
    public class AssetProvider : IAssetProvider
    {
        private readonly IPrefabLoader _prefabLoader;

        public AssetProvider(IPrefabLoader prefabLoader)
        {
            _prefabLoader = prefabLoader;
        }
        
        public TType Instantiate<TType>(string path) where TType : Object => 
            Object.Instantiate(_prefabLoader.LoadPrefab<TType>(path));
        
        public TType Instantiate<TType>(string path, Transform parent) where TType : Object => 
            Object.Instantiate(_prefabLoader.LoadPrefab<TType>(path), parent);
        
        public GameObject Instantiate(string path) =>  
            (GameObject) Object.Instantiate(_prefabLoader.LoadPrefab(path));
        
        public GameObject Instantiate(string path, Transform parent) =>
            (GameObject) Object.Instantiate(_prefabLoader.LoadPrefab(path), parent);

        
    }
}