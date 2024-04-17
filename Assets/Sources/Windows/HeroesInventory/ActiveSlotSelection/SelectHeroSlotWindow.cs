using Infrastructure.PrefabPaths;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.Hero;
using Infrastructure.Services.AssetsProvider;
using Sources.HeroBase.HeroesFieldBase;
using Sources.HeroBase.HeroSlotBase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Windows.HeroesInventory.ActiveSlotSelection
{
    public class SelectHeroSlotWindow : WindowBase, IPayloadWindow<HeroData>
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _slotStorage;
        
        private HeroesField _heroesField;
        private IAssetProvider _assetProvider;

        [Inject]
        public void Construct(HeroesField heroesField, IAssetProvider assetProvider)
        {
            _heroesField = heroesField;
            _assetProvider = assetProvider;
        }

        public void ConstructPayload(HeroData payload)
        {
            for (int i = 0; i < _heroesField.ActiveHeroesSlots.Length; i++)
            {
                var instance = _assetProvider.Instantiate<Button>(HeroPaths.SelectHeroSlot, _slotStorage);
                HeroSlot slotToPlace = _heroesField.ActiveHeroesSlots[i];

                instance.GetComponentInChildren<TMP_Text>().text = $"{i + 1}";
                instance.onClick.AddListener((() =>
                {
                    slotToPlace.SetActiveHero(payload);
                    Close();
                }));
            }
        }

        protected override void Close()
        {
            base.Close();
            Destroy(gameObject);   
        }

        protected override void OnEnabling() => 
            _closeButton.onClick.AddListener(Close);

        protected override void OnDisabling() => 
            _closeButton.onClick.RemoveAllListeners();
    }
}