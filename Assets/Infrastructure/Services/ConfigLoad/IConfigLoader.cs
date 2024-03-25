using Infrastructure.Configurations;
using UnityEngine;

namespace Infrastructure.Services.ConfigLoad
{
    public interface IConfigLoader
    {
        /// <summary>
        /// Load some configuration from Assets/Resources/Configurations/ directory
        /// </summary>
        /// <typeparam name="TConfig">ScriptableObject with IConfiguration interface mark</typeparam>
        /// <returns>Returns config loaded from the config's directory</returns>
        TConfig LoadConfiguration<TConfig>() where TConfig : ScriptableObject, IConfiguration;
        
        /// <summary>
        /// Load some configuration from Assets/Resources/Configurations/ + "Path" directory
        /// </summary>
        /// <param name="path">Path to config directory from Assets/Resources/Configurations/</param>
        TConfig LoadConfiguration<TConfig>(string path) where TConfig : ScriptableObject, IConfiguration;
    }
}