using UnityEngine;

namespace Infrastructure.Services.PrefabLoad
{
    /// <summary>
    /// Service is loading prefabs from resources.
    /// </summary>
    public interface IPrefabLoader : IService
    {
        Object LoadPrefab(string path);
        TType LoadPrefab<TType>(string path) where TType : Object;
        TType[] LoadAllPrefabs<TType>(string path) where TType : Object;
    }
}