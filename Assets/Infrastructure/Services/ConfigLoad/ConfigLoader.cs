using System.IO;
using Infrastructure.Services.PrefabLoad;
using UnityEngine;

namespace Infrastructure.Services.ConfigLoad
{
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

            if (results.Length == 0)
                return null;

            return results[0];
        }

        public TConfig LoadConfiguration<TConfig>(string path) where TConfig : ScriptableObject, IConfiguration
        {
            string searchingPath = Path.Combine(ConfigsPath, path);
            TConfig[] results = _prefabLoader.LoadAllPrefabs<TConfig>(searchingPath);

            if (results.Length == 0)
                return null;

            return results[0];
        }
    }
}