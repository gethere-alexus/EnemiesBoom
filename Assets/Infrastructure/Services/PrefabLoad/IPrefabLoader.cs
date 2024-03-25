using UnityEngine;

namespace Infrastructure.Services.PrefabLoad
{
    public interface IPrefabLoader
    {
        Object LoadPrefab(string path);
        TType LoadPrefab<TType>(string path) where TType : Object;
        TType[] LoadAllPrefabs<TType>(string path) where TType : Object;
    }
}