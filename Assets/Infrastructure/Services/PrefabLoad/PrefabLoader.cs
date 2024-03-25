using UnityEngine;

namespace Infrastructure.Services.PrefabLoad
{
    public class PrefabLoader : IPrefabLoader
    {
        public Object LoadPrefab(string path) => 
            Resources.Load(path);
        
        public TType LoadPrefab<TType>(string path) where TType : Object => 
            Resources.Load<TType>(path);

        public TType[] LoadAllPrefabs<TType>(string path) where TType : Object => 
            Resources.LoadAll<TType>(path);
    }
}