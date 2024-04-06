using System.Collections.Generic;
using System.IO;
using Infrastructure.Configurations.InitialProgress;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.AnvilData;
using Infrastructure.ProgressData.Field;
using Infrastructure.Services.ProgressLoad.Connection;
using UnityEngine;

namespace Infrastructure.Services.ProgressLoad
{
    public class ProgressProvider : IProgressProvider
    {
        public List<IProgressReader> ProgressReaders { get; } = new List<IProgressReader>();

        public List<IProgressWriter> ProgressWriters { get; } = new List<IProgressWriter>();

        private GameProgress _gameProgress;

        private const string SaveName = "GameProgress.json";

        private readonly string _savePath;

        public ProgressProvider()
        {
            _savePath = Path.Combine(Application.persistentDataPath, SaveName);
            _gameProgress = LoadProgress();
        }


        public void SaveProgress()
        {
            foreach (var progressWriter in ProgressWriters)
            {
                progressWriter.SaveProgress(_gameProgress);
            }

            string json = JsonUtility.ToJson(_gameProgress);
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
            InitialProgressContent progressContent =
                Resources.Load<InitialProgressContainer>("DataBase/ProgressContainer").InitProgressContent;
            
            GameProgress progress = new GameProgress()
            {
                Anvil = progressContent.Anvil,
                GameField = new GameFieldData()
                {
                    Grid = progressContent.InitialFieldData.ToArray(),
                },
                AnvilExtensions = new AnvilExtensionsData()
                {
                    AnvilAutoRefiller  = progressContent.AutoRefiller,
                    AnvilAutoUse = progressContent.AnvilAutoUsing,
                    AnvilRefill = progressContent.AnvilRefilling
                },
                FieldExtensions = new FieldExtensionsData()
                {
                    SlotsAutoMerger  = progressContent.AutoMerger,
                },
            };

            return progress;
        }

        public GameProgress GameProgress => _gameProgress;
    }
}