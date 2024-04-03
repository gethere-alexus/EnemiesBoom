using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.Extensions.EditorExtensions
{
#if UNITY_EDITOR
    /// <summary>
    /// Editor extension for comfortable working with save files
    /// </summary>
    public static class ProgressManagement
    {
        private const string SaveName = "GameProgress.json";
        [MenuItem("Progress Management/Clean Progress")]
        public static void CleanProgress()
        {
            string savesPath = Path.Combine(Application.persistentDataPath, SaveName);
            File.Delete(savesPath);
        }

        [MenuItem("Progress Management/Show Progress")]
        public static void OpenSave()
        {
            string savesPath = Path.Combine(Application.persistentDataPath, SaveName);
            Process.Start("explorer.exe", savesPath.Replace("/", "\\"));
        }
    }
#endif
}