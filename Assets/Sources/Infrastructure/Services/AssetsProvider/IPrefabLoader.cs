using UnityEngine;

namespace Sources.Infrastructure.Services.AssetsProvider
{
    public interface IPrefabLoader
    {
        Object LoadPrefab(string path);
        TType LoadPrefab<TType>(string path) where TType : Object;
    }
}