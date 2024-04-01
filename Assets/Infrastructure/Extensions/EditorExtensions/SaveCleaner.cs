using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.Extensions.EditorExtensions
{
    /// <summary>
    /// Editor extension for comfortable working with save files
    /// </summary>
    public static class ProgressManagement
    {
        private const string Saves = "Saves";

        [MenuItem("Progress Management/Clean Progress")]public static void CleanProgress()
        {
            string savesPath = Path.Combine(Application.persistentDataPath, Saves);

            foreach (var path in Directory.GetFiles(savesPath))
            {
                    File.Delete(path);
            }
            
        }
        
        [MenuItem("Progress Management/Show Progress")]public static void OpenSave()
        {
            string savesPath = Path.Combine(Application.persistentDataPath, Saves);
            Process.Start("explorer.exe", savesPath.Replace("/", "\\"));
        }
    }
}