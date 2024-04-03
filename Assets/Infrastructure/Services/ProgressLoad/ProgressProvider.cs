using System.Collections.Generic;
using System.IO;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.AnvilData;
using Infrastructure.ProgressData.Field;
using Infrastructure.ProgressData.Field.Slot;
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

        private GameProgress LoadProgress()
        {
            if (File.Exists(_savePath))
            {
                string json = File.ReadAllText(_savePath);
                GameProgress toReturn = JsonUtility.FromJson<GameProgress>(json);
                return toReturn;
            }
            else
            {
                return GetBaseProgress();
            }
        }

        private GameProgress GetBaseProgress()
        {
            SlotData[] baseGrid = new SlotData[40];
            int toUnlock = 11;
            for (int i = 0; i < baseGrid.Length; i++)
            {
                baseGrid[i] = new SlotData()
                {
                    IsLocked = i > toUnlock,
                };
            }   

            GameProgress baseProgress = new GameProgress()
            {
                GameField = new GameFieldData()
                {
                    Grid = baseGrid,
                },
                FieldExtensions = new FieldExtensionsData()
                {
                    SlotsAutoMerger = new SlotsAutoMergerData()
                    {
                        UsageCoolDown = 25.0f,
                    },
                },
                Anvil = new AnvilData()
                {
                    MaxCharges = 10,
                    ChargesLeft = 10,
                    CraftingItemLevel = 1,
                },
                AnvilExtensions = new AnvilExtensionsData()
                {
                    AnvilAutoRefiller = new AnvilAutoRefillerData()
                    {
                        AmountChargesToAdd = 1,
                        RefillCoolDown = 10.0f,
                    },
                    AnvilAutoUse = new AnvilAutoUseData()
                    {
                        UsingCoolDown = 15.0f,
                    },
                    AnvilRefill = new AnvilRefillData()
                    {
                        Charges = 10,
                    },
                },
            };

            return baseProgress;
        }

        public GameProgress GameProgress => _gameProgress;
    }
}