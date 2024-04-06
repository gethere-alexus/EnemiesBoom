using System;
using Infrastructure.Configurations.Config;
using Infrastructure.Services.ConfigLoad;
using UnityEngine;

namespace Sources.HeroBase.Storing.Holder
{
    public class HeroSlotsHolder : IConfigReader
    {
        [SerializeField] private HeroSlot[] _heroSlots;
        public event Action ConfigLoaded;
        public void LoadConfiguration(ConfigContent configContainer)
        {
            _heroSlots = new HeroSlot[configContainer.ActiveHeroesHolder.HeroesHolders];
            ConfigLoaded?.Invoke();
        }

        public HeroSlot[] HeroSlots => _heroSlots;
    }
}