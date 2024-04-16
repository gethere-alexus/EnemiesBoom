using System;
using Infrastructure.ProgressData;
using Infrastructure.ProgressData.Hero;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Windows.HeroesInventory
{
    public class HeroCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _heroNameText;
        [SerializeField] private TMP_Text _heroDamageText;
        [SerializeField] private TMP_Text _heroReloadSpeedText;
        [SerializeField] private TMP_Text _heroPurchaseText;
        [SerializeField] private Button _interactHeroButton;

        public void Construct(HeroData heroData, bool isPurchased, Action<HeroData> onSelected = null, Action<HeroData> onPurchasing = null)
        {
            ConstructDisplay(heroData,isPurchased);

            if (isPurchased)
                ConstructSelectionButton(onSelected, heroData);
            else
               ConstructPurchaseButton(onPurchasing, heroData);
        }
        private void ConstructSelectionButton(Action<HeroData> onSelected, HeroData heroData) =>
            _interactHeroButton.onClick.AddListener((() => onSelected?.Invoke(heroData)));

        private void ConstructPurchaseButton(Action<HeroData> onPurchasing, HeroData heroData)
        {
            _interactHeroButton.GetComponentInChildren<TMP_Text>().text = "Purchase";
            _interactHeroButton.onClick.AddListener(() => onPurchasing?.Invoke(heroData));
        }
        private void ConstructDisplay(HeroData heroData, bool isPurchased)
        {
            _heroNameText.text = heroData.Name;
            _heroDamageText.text = $"Damage: {heroData.Damage}";
            _heroReloadSpeedText.text = $"Reload: {heroData.ReloadSpeed}s";
            _heroPurchaseText.text = $"Bought: {isPurchased}";
        }
        private void OnDisable() => 
            _interactHeroButton.onClick.RemoveAllListeners();
    }
}