using System.IO;
using Infrastructure.Configurations;
using Infrastructure.Services.PrefabLoad;
using UnityEngine;

namespace Infrastructure.Services.ConfigLoad
{
    /// <summary>
    /// Loading configs, before using make sure to have it existing and set up. 
    /// </summary>
    public class ConfigLoader : IConfigLoader
    {
        private const string ConfigsPath = "Configurations/";
        private readonly IPrefabLoader _prefabLoader;

        public ConfigLoader(IPrefabLoader prefabLoader)
        {
            _prefabLoader = prefabLoader;
        }

        public TConfig LoadConfiguration<TConfig>() where TConfig : ScriptableObject, IConfiguration
        {
            TConfig[] results = _prefabLoader.LoadAllPrefabs<TConfig>(ConfigsPath);
            LogErrors(results);
            
            if (results.Length == 0)
                return null;
            
            return results[0];
        }

        public TConfig LoadConfiguration<TConfig>(string path) where TConfig : ScriptableObject, IConfiguration
        {
            string searchingPath = Path.Combine(ConfigsPath, path);
            Debug.Log(searchingPath);
            TConfig[] results = _prefabLoader.LoadAllPrefabs<TConfig>(searchingPath);
            LogErrors(results);
            
            if (results.Length == 0)
                return null;
            
            return results[0];
        }


        /// <summary>
        /// Logging errors if there are such ones
        /// </summary>
        private void LogErrors<TConfig>(TConfig[] loadResults) where TConfig : ScriptableObject, IConfiguration
        {
            if (loadResults.Length > 1)
                Debug.LogWarning($"Found {loadResults.Length} config results, returning the first one");
            else if (loadResults.Length == 0)
            {
                Debug.LogError($"{typeof(TConfig)} configuration is missing !");
            }
        }
    }
}