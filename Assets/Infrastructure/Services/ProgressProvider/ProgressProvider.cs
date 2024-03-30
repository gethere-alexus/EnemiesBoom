using System.IO;
using Infrastructure.ProgressData;
using JetBrains.Annotations;
using UnityEngine;

namespace Infrastructure.Services.ProgressProvider
{
    public class ProgressProvider : IProgressProvider
    {
        private const string SaveDirectoryName = "Saves";
        private const string SavingFileExtension = "json";

        private string _saveDirectory;

        public ProgressProvider()
        {
            _saveDirectory = Path.Combine(Application.persistentDataPath, SaveDirectoryName);
            TryCreateDirectory(_saveDirectory);
        }

        public void SaveProgress<TProgress>(TProgress progress) where TProgress : IProgressData
        {
            string file = $"{typeof(TProgress).Name}.{SavingFileExtension}";
            string savePath = Path.Combine(_saveDirectory, file);

            var jsonSave = JsonUtility.ToJson(progress);
            File.WriteAllText(savePath, jsonSave);
        }

        [CanBeNull]
        public TProgress LoadProgress<TProgress>() where TProgress : IProgressData
        {
            string searchingFile = $"{typeof(TProgress).Name}.{SavingFileExtension}";
            string searchAt = Path.Combine(_saveDirectory, searchingFile);
            
            if (File.Exists(searchAt))
            {
                string json = File.ReadAllText(searchAt);
                TProgress progress = JsonUtility.FromJson<TProgress>(json);
                return progress;
            }

            return default;
        }

        /// <summary>
        /// Creates a directory if it doesn't exist
        /// </summary>
        private void TryCreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }
    }
}