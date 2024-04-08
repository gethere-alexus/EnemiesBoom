using NorskaLib.Spreadsheets;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.Extensions.EditorExtensions
{
#if UNITY_EDITOR
    public static class ConfigUpdater
    {
        private const string ConfigsPath = "DataBase";
        [MenuItem("Config Loader / Update Configs")]
        public static void UpdateConfigs()
        {
            foreach (var container in Resources.LoadAll<SpreadsheetsContainerBase>(ConfigsPath))
            {
              
            }
        }
    }
#endif
}