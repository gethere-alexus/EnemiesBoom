using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Configurations.Config;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.AnvilData;
using Infrastructure.ProgressData.Field;
using Infrastructure.ProgressData.Field.Slot;
using Infrastructure.ProgressData.Hero;
using Infrastructure.ProgressData.Item;
using Infrastructure.Services.UpgradeRegistry;
using Infrastructure.Services.UpgradeRegistry.Upgrades.AnvilAutoUseUpgrade;
using Infrastructure.Services.UpgradeRegistry.Upgrades.AnvilReloadSpeedUpgrade;
using Infrastructure.Services.UpgradeRegistry.Upgrades.CraftingItemLevel;
using Infrastructure.Services.UpgradeRegistry.Upgrades.SlotMergeUpgrade;
using NorskaLib.Spreadsheets;

namespace Infrastructure.Configurations.InitialProgress
{
    [Serializable]
    public class InitialProgressContent
    {
        [SpreadsheetPage("InitField")] public List<SlotData> InitialFieldData;

        [SpreadsheetPage("ActiveHeroesHolder")]
        public ActiveHeroesConfig ActiveHeroesSlots;

        [SpreadsheetPage("Upgrades")] public List<UpgradeConfiguration> UpgradeData;
        [SpreadsheetPage("Heroes")] public List<HeroData> Heroes;
        [SpreadsheetPage("AutoMerge")] public SlotsAutoMergerData AutoMerger;
        [SpreadsheetPage("Anvil")] public AnvilData Anvil;
        [SpreadsheetPage("AnvilAutoRefiller")] public AnvilAutoRefillerData AutoRefiller;
        [SpreadsheetPage("AnvilRefill")] public AnvilRefillData AnvilRefilling;
        [SpreadsheetPage("AnvilAutoUse")] public AnvilAutoUseData AnvilAutoUsing;
        [SpreadsheetPage("Wallet")] public WalletData WalletData;

        public GameProgress GetInitialProgress()
        {
            GameProgress toReturn = new GameProgress()
            {
                Anvil = new AnvilData()
                {
                    ChargesLeft = Anvil.ChargesLeft,
                    CraftingItemLevel = Anvil.CraftingItemLevel,
                    MaxCharges = Anvil.MaxCharges,
                },
                UpgradesData = new UpgradesData()
                {
                    AnvilItemUpgradeData = new UpgradeData()
                    {
                        CurrentUpgradeStage = UpgradeData.Find(data =>
                            data.UpgradeID == AnvilCraftingItemUpgrade.UpgradeID).StartUpgradeStage,
                    },
                    AnvilReloadUpgradesData = new UpgradeData()
                    {
                        CurrentUpgradeStage = UpgradeData.Find(data =>
                            data.UpgradeID == AnvilReloadSpeedUpgrade.UpgradeID).StartUpgradeStage,
                    },
                    AnvilAutoUseUpgradesData = new UpgradeData()
                    {
                        CurrentUpgradeStage = UpgradeData.Find(data =>
                            data.UpgradeID == AnvilAutoUseUpgrade.UpgradeID).StartUpgradeStage,
                    },
                    SlotsMergeUpgradeData = new UpgradeData()
                    {
                        CurrentUpgradeStage = UpgradeData.Find(data =>
                            data.UpgradeID == SlotsMergeUpgrade.UpgradeID).StartUpgradeStage,
                    },
                },
                ItemField = new ItemFieldData()
                {
                    Grid = InitialFieldData.ToArray(),
                },
                WalletData = new WalletData()
                {
                    Balance = WalletData.Balance,
                },
                HeroesData = new HeroesData()
                {
                    ActiveHeroes = GetInitiallyActiveHeroes(),
                    PurchasedHeroesIDs = GetInitialHeroesIDs(),
                },
                AnvilExtensions = new AnvilExtensionsData()
                {
                    AnvilAutoRefiller = new AnvilAutoRefillerData()
                    {
                        AmountChargesToAdd = AutoRefiller.AmountChargesToAdd,
                        RefillCoolDown = AutoRefiller.RefillCoolDown,
                    },
                    
                    AnvilAutoUse = new AnvilAutoUseData()
                    {
                      UsingCoolDown  = AnvilAutoUsing.UsingCoolDown,
                    },
                    AnvilRefill = new AnvilRefillData()
                    {
                        Charges = AnvilRefilling.Charges,
                    }
                },
                FieldExtensions = new FieldExtensionsData()
                {
                    SlotsAutoMerger = new SlotsAutoMergerData()
                    {
                        UsageCoolDown = AutoMerger.UsageCoolDown,
                    }
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