using System;

namespace Infrastructure.ProgressData
{
    [Serializable]
    public class HeroesProgress
    {
        public ActiveHeroData[] ActiveHeroes;
        public int[] PurchasedHeroesIDs;
    }
}