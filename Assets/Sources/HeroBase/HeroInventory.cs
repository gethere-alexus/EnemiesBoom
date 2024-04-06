using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Configurations.Config;
using Infrastructure.ProgressData;
using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.ProgressLoad.Connection;

namespace Sources.HeroBase
{
    [Serializable]
    public class HeroInventory : IProgressWriter, IConfigReader
    {
        private readonly List<HeroData> _heroes = new List<HeroData>();
        private readonly List<HeroData> _purchasedHeroes = new List<HeroData>();

        public event Action ConfigLoaded;

        public HeroInventory()
        {
           
        }

        public void LoadProgress(GameProgress progress)
        {
            var purchasedHeroes = _heroes.Where(hero => progress.PurchasedHeroesID.Contains(hero.ID) || hero.IsInitial);
            
            foreach (var heroToAdd in purchasedHeroes)
            {
                _purchasedHeroes.Add(heroToAdd);
            }
        }

        public void SaveProgress(GameProgress progress) => 
            SaveHeroesIDs(progress);

        private void SaveHeroesIDs(GameProgress progress)
        {
            int[] IDs = new int[_purchasedHeroes.Count];
            for (int i = 0; i < _purchasedHeroes.Count; i++)
            {
                IDs[i] = _purchasedHeroes[i].ID;
            }

            progress.PurchasedHeroesID = IDs;
        }

        public void LoadConfiguration(ConfigContent configContainer)
        {
            _heroes.Clear();
            _heroes.AddRange(configContainer.Heroes);
            ConfigLoaded?.Invoke();
        }
    }
}