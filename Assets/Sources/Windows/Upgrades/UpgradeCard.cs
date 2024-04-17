using System;
using Infrastructure.Services.UpgradeRegistry;
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

        public void ConstructDisplay(IUpgradable upgradable, UpgradeConfiguration configuration, Action<IUpgradable> onUpgrading)
        {
            _upgradeName.text = configuration.UpgradeName;
            _upgradeDescription.text = configuration.UpgradeDescription;

            string stageProgress = $"{upgradable.CurrentUpgradeStage}/{configuration.MaxUpgradeStage}";
            _stageProgressText.text = stageProgress;
            
            _progressSlider.minValue = configuration.StartUpgradeStage;
            _progressSlider.maxValue = configuration.MaxUpgradeStage;
            _progressSlider.value = upgradable.CurrentUpgradeStage;

            string price = upgradable.IsCompletelyUpgraded ? "Max." : upgradable.UpgradePrice.ToString();
            _upgradePrice.text = price;
            _upgradeButton.onClick.AddListener(() => onUpgrading?.Invoke(upgradable));
        }

        private void OnDisable() => 
            _upgradeButton.onClick.RemoveAllListeners();
    }
}