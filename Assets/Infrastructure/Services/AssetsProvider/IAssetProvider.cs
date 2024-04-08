using UnityEngine;

namespace Infrastructure.Services.AssetsProvider
{
    public interface IAssetProvider : IService
    {
        /// <summary>
        /// Instantiating the object by its type.
        /// </summary>
        /// <param name="path">Path to the object from resources directory</param>
        /// <returns></returns>
        TType Instantiate<TType>(string path) where TType : Object;
        
        /// <summary>
        /// Instantiating the object by its type with parenting
        /// </summary>
        /// <param name="path">Path to the object from resources directory</param>
        /// <returns></returns>
        TType Instantiate<TType>(string path, Transform parent) where TType : Object;
        
        /// <summary>
        /// Instantiate as a GameObject
        /// </summary>
        /// <param name="path">Path to the object from resources directory</param>
        /// <returns>GameObject</returns>
        GameObject Instantiate(string path);
        
        /// <summary>
        /// Instantiate as a GameObject with a parent.
        /// </summary>
        /// <param name="path">Path to the object from resources directory</param>
        /// <returns>GameObject</returns>
        GameObject Instantiate(string path, Transform parent);
    }
}