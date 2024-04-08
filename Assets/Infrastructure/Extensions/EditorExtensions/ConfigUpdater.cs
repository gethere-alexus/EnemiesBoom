using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NorskaLib.Spreadsheets;
using UnityEditor;
using UnityEngine;
using Object = System.Object;
using SpreadsheetImporter = NorskaLib.Spreadsheets.SpreadsheetImporter;

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