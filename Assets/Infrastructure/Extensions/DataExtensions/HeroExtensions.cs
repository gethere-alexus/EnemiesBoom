using System.Collections.Generic;
using System.Linq;
using Infrastructure.ProgressData;

namespace Infrastructure.Extensions.DataExtensions
{
    /// <summary>
    /// Exposing API for HeroData.
    /// </summary>
    public static class HeroExtensions
    {
        /// <summary>
        /// Searches for a hero by its ID in an array of heroes, return null if not found.
        /// </summary>
        public static HeroData GetHeroByID(this HeroData[] array, int id) =>
            array.FirstOrDefault(data => data.ID == id);
        
        /// <summary>
        /// Searches for a hero by its ID in a list of heroes, return null if not found.
        /// </summary>
        public static HeroData GetHeroByID(this List<HeroData> array, int id) =>
            array.FirstOrDefault(data => data.ID == id);
    }
}