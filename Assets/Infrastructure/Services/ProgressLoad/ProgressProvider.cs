using System.Collections.Generic;
using System.IO;
using Infrastructure.Configurations.InitialProgress;
using Infrastructure.PrefabPaths;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.AnvilData;
using Infrastructure.ProgressData.Field;
using Infrastructure.Services.PrefabLoad;
using Infrastructure.Services.ProgressLoad.Connection;
using UnityEngine;

namespace Infrastructure.Services.ProgressLoad
{
    public class ProgressProvider : IProgressProvider
    {
        private readonly IPrefabLoader _prefabLoader;
        
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();
        public List<IProgressWriter> ProgressWriters { get; } = new List<IProgressWriter>();

        private GameProgress _gameProgress;

        private const string SaveName = "GameProgress.json";

        private readonly string _savePath;

        public ProgressProvider(IPrefabLoader prefabLoader)
        {
            _prefabLoader = prefabLoader;
            
            _savePath = Path.Combine(Application.persistentDataPath, SaveName);
            _gameProgress = LoadProgress();
        }


        public void SaveProgress()
        {
            foreach (var progressWriter in ProgressWriters)
            {
                progressWriter.SaveProgress(_gameProgress);
            }

            string json = JsonUtility.ToJson(_gameProgress, true);
            File.WriteAllText(_savePath, json);
        }

        public void RegisterObserver(IProgressReader reader)
        {
            ProgressReaders.Add(reader);

            if (reader is IProgressWriter writer)
                ProgressWriters.Add(writer);
        }

        public void ClearObservers()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameProgress LoadProgress()
        {
            GameProgress toReturn;
            if (File.Exists(_savePath))
            {
                string json = File.ReadAllText(_savePath);
                toReturn = JsonUtility.FromJson<GameProgress>(json);
            }
            else
            {
                toReturn = GetInitialProgress();
            }

            return toReturn;
        }

        private GameProgress GetInitialProgress()
        {
            GameProgress toReturn = _prefabLoader.LoadPrefab<InitialProgressContainer>(PersistentDataPaths.InitialProgress)
                .InitProgressContent.GetInitialProgress();
            return toReturn;
        }
        public GameProgress GameProgress => _gameProgress;
    }
}