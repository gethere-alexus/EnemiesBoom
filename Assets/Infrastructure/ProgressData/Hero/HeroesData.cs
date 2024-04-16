using System;

namespace Infrastructure.ProgressData.Hero
{
    [Serializable]
    public class HeroesData
    {
        public ActiveHeroData[] ActiveHeroes;
        public int[] PurchasedHeroesIDs;
    }
}