using System.Collections.Generic;
using Infrastructure.Configurations.Config;
using Infrastructure.PrefabPaths;
using Infrastructure.Services.PrefabLoad;

namespace Infrastructure.Services.ConfigLoad
{
    public class ConfigLoader : IConfigLoader
    {
        private readonly List<IConfigReader> _configReaders = new List<IConfigReader>();
        private readonly ConfigContent _configContent;

        
        public ConfigLoader(IPrefabLoader prefabLoader) => 
            _configContent = prefabLoader.LoadPrefab<ConfigContainer>(PersistentDataPaths.GameConfig).ConfigContent;

        public void LoadConfigs()
        {
            foreach (var configReader in _configReaders)
            {
                configReader.LoadConfiguration(_configContent);
            }
        }

        public void RegisterLoader(IConfigReader configLoader) => 
            _configReaders.Add(configLoader);

        public void ClearLoaders() => 
            _configReaders.Clear();
    }
}