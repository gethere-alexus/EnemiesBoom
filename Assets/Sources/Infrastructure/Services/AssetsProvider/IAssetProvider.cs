using UnityEngine;

namespace Sources.Infrastructure.Services.AssetsProvider
{
    public interface IAssetProvider : IPrefabLoader
    {
        TType Instantiate<TType>(string path) where TType : Object;
        TType Instantiate<TType>(string path, Transform parent) where TType : Object;
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Transform parent);
    }
}