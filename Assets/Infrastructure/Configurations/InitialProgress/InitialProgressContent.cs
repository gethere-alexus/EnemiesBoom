using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Configurations.Config;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.AnvilData;
using Infrastructure.ProgressData.Field;
using Infrastructure.ProgressData.Field.Slot;
using Infrastructure.ProgressData.Item;
using NorskaLib.Spreadsheets;

namespace Infrastructure.Configurations.InitialProgress
{
    [Serializable]
    public class InitialProgressContent
    {
        [SpreadsheetPage("InitField")] public List<SlotData> InitialFieldData;
        [SpreadsheetPage("ActiveHeroesHolder")] public ActiveHeroesConfig ActiveHeroesSlots;
        [SpreadsheetPage("Heroes")] public List<HeroData> Heroes;
        [SpreadsheetPage("AutoMerge")] public SlotsAutoMergerData AutoMerger;
        [SpreadsheetPage("Anvil")] public AnvilData Anvil;
        [SpreadsheetPage("AnvilAutoRefiller")] public AnvilAutoRefillerData AutoRefiller;
        [SpreadsheetPage("AnvilRefill")] public AnvilRefillData AnvilRefilling;
        [SpreadsheetPage("AnvilAutoUse")] public AnvilAutoUseData AnvilAutoUsing;

        public GameProgress GetInitialProgress()
        {
            GameProgress toReturn = new GameProgress()
            {
                Anvil = Anvil,
                GameField = new GameFieldData()
                {
                    Grid = InitialFieldData.ToArray(),
                },
                HeroesProgress = new HeroesProgress()
                {
                    ActiveHeroes = GetInitiallyActiveHeroes(),
                    PurchasedHeroesIDs = GetInitialHeroesIDs(),
                },
                AnvilExtensions = new AnvilExtensionsData()
                {
                    AnvilAutoRefiller = AutoRefiller,
                    AnvilAutoUse = AnvilAutoUsing,
                    AnvilRefill = AnvilRefilling,
                },
                FieldExtensions = new FieldExtensionsData()
                {
                    SlotsAutoMerger = AutoMerger,
                },
            };
            return toReturn;
        }

        private HeroData[] InitialHeroes => Heroes.Where(hero => hero.IsInitial).ToArray();

        private ActiveHeroData[] GetInitiallyActiveHeroes()
        {
            int[] initialHeroesIDs = GetInitialHeroesIDs();
            ActiveHeroData[] activeHeroes = new ActiveHeroData[ActiveHeroesSlots.AvailableAmountOfSlots];

            for (int i = 0; i < activeHeroes.Length; i++)
            {
                int idToPlace = i < initialHeroesIDs.Length ? initialHeroesIDs[i] : -1;
                activeHeroes[i] = new ActiveHeroData
                {
                    HeroID = idToPlace,
                    StoredItem = new ItemData()
                    {
                        Level = 1,
                    }
                };
            }

            return activeHeroes;
        }

        private int[] GetInitialHeroesIDs()
        {
            int[] toReturn = new int[InitialHeroes.Length];
            for (int i = 0; i < InitialHeroes.Length; i++)
            {
                toReturn[i] = InitialHeroes[i].ID;
            }

            return toReturn;
        }
    }
}