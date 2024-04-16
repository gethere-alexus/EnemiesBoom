using System;
using Infrastructure.Configurations.Config;
using Infrastructure.Extensions.DataExtensions;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.Hero;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.ProgressLoad.Connection;
using Sources.HeroBase.HeroSlotBase;
using Sources.ItemsBase.ItemBase;
using Sources.ItemsBase.ItemFieldBase;

namespace Sources.HeroBase.HeroesFieldBase
{
    public class HeroesField : IConfigReader, IProgressWriter
    {
        private HeroSlot[] _activeHeroesSlots;
        private readonly HeroesInventory _heroesInventory;
        private readonly ItemField _itemField;

        public event Action ConfigLoaded;

        public HeroesField(HeroesInventory heroesInventory,ItemField itemField)
        {
            _heroesInventory = heroesInventory;
            _itemField = itemField;
        }
        public void LoadConfiguration(ConfigContent configContainer)
        {
            _activeHeroesSlots = new HeroSlot[configContainer.ActiveHeroesSlots.AvailableAmountOfSlots];
            
            for (int i = 0; i < configContainer.ActiveHeroesSlots.AvailableAmountOfSlots; i++)
            {
                _activeHeroesSlots[i] = new HeroSlot(_itemField);
            }
            
            ConfigLoaded?.Invoke();
        }

        public void LoadProgress(GameProgress progress)
        {
            for (int i = 0; i < _activeHeroesSlots.Length; i++)
            {
                if (i < progress.HeroesData.ActiveHeroes.Length)
                {
                    int heroID = progress.HeroesData.ActiveHeroes[i].HeroID;
                    HeroData heroToPlace = _heroesInventory.Heroes.GetHeroByID(heroID);

                    Item itemToPlace = progress.HeroesData.ActiveHeroes[i].StoredItem.FromSerializable();

                    _activeHeroesSlots[i].SetActiveHero(heroToPlace);
                    _activeHeroesSlots[i].SetStoredItem(itemToPlace);
                }
            }
        }

        public void SaveProgress(GameProgress progress)
        {
            progress.HeroesData.ActiveHeroes = _activeHeroesSlots.ToSerializable();
        }

        public HeroSlot[] ActiveHeroesSlots => _activeHeroesSlots;
    }
}