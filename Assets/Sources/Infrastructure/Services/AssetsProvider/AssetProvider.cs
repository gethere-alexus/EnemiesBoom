using UnityEngine;

namespace Sources.Infrastructure.Services.AssetsProvider
{
    public class AssetProvider : IAssetProvider
    {
        // instantiate generic object
        public TType Instantiate<TType>(string path) where TType : Object => 
            Object.Instantiate(LoadPrefab<TType>(path));
        
        // instantiate generic object and provide parent
        public TType Instantiate<TType>(string path, Transform parent) where TType : Object => 
            Object.Instantiate(LoadPrefab<TType>(path), parent);

        // instantiate GameObject
        public GameObject Instantiate(string path) =>  
            (GameObject) Object.Instantiate(LoadPrefab(path));

        // instantiate GameObject and provide parent
        public GameObject Instantiate(string path, Transform parent) =>
            (GameObject) Object.Instantiate(LoadPrefab(path), parent);

        public Object LoadPrefab(string path) => 
            Resources.Load(path);
        public TType LoadPrefab<TType>(string path) where TType : Object=> 
            Resources.Load<TType>(path);
    }
}