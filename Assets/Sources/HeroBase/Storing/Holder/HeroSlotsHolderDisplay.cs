using Infrastructure.Services.AssetsProvider;
using UnityEngine;

namespace Sources.HeroBase.Storing.Holder
{
    public class HeroSlotsHolderDisplay : MonoBehaviour
    {
        [SerializeField] private Transform _heroesHolder;
        
        private IAssetProvider _assetProvider;
        private HeroSlotsHolder _heroSlotsHolderInstance;

        public void Construct(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
            
            _heroSlotsHolderInstance = new HeroSlotsHolder();
            _heroSlotsHolderInstance.ConfigLoaded += BuildHeroHolders;
        }

        private void BuildHeroHolders()
        {
            ClearStorage();
            InstantiateHolders();
        }

        private void InstantiateHolders()
        {
            for (int i = 0; i < _heroSlotsHolderInstance.HeroSlots.Length; i++)
            {
                var slotDisplay = _assetProvider.Instantiate<HeroSlotDisplay>
                    ("Prefabs/UI/HeroSlot/HeroSlot", _heroesHolder);
                
                slotDisplay.Construct(_heroSlotsHolderInstance.HeroSlots[i]);
            }
        }

        private void ClearStorage()
        {
            foreach (Transform child in _heroesHolder)
            {
                Destroy(child.gameObject);
            }
        }

        private void OnDisable() => 
            _heroSlotsHolderInstance.ConfigLoaded -= BuildHeroHolders;

        public HeroSlotsHolder HeroSlotsHolderInstance => _heroSlotsHolderInstance;
    }
}