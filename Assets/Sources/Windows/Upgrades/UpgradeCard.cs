using System;
using Infrastructure.Services.UpgradeRegistry;
using Infrastructure.Services.UpgradeRegistry.Upgrades;
using Sources.WalletBase;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Sources.Windows.Upgrades
{
    public class UpgradeCard : MonoBehaviour
    {
        [SerializeField] private TMP_Text _upgradeName, _upgradeDescription;

        [SerializeField] private TMP_Text _stageProgressText;
        [SerializeField] private Slider _progressSlider;

        [SerializeField] private TMP_Text _upgradePrice;
        [SerializeField] private Button _upgradeButton;

        public void ConstructDisplay(UpgradeBase upgradable, UpgradeConfiguration configuration, Action<UpgradeBase> onUpgrading)
        {
            _upgradeName.text = configuration.UpgradeName;
            _upgradeDescription.text = configuration.UpgradeDescription;

            string stageProgress = $"{upgradable.CurrentUpgradeStage}/{configuration.MaxUpgradeStage}";
            _stageProgressText.text = stageProgress;
            
            _progressSlider.minValue = configuration.StartUpgradeStage;
            _progressSlider.maxValue = configuration.MaxUpgradeStage;
            _progressSlider.value = upgradable.CurrentUpgradeStage;

            string price = upgradable.IsCompletelyUpgraded ? "Max." : WalletUtility.ConvertToPrettyFormat(upgradable.UpgradePrice, ',');
            _upgradePrice.text = price;
            _upgradeButton.onClick.AddListener(() => onUpgrading?.Invoke(upgradable));
        }

        private void OnDisable() => 
            _upgradeButton.onClick.RemoveAllListeners();
    }
}