using Infrastructure.PrefabPaths;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.UpgradeRegistry;
using Sources.Utils;
using Sources.WalletBase;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources.Windows.Upgrades
{
    public class UpgradesWindow : WindowBase
    {
        [SerializeField] private Button _closeButton;
        [SerializeField] private Transform _contentStorage;
        
        private IWallet _wallet;
        private IUpgradesRegistry _upgradesRegistry;
        private IAssetProvider _assetProvider;

        [Inject]
        public void Construct(IUpgradesRegistry upgradesRegistry, IAssetProvider assetProvider, IWallet wallet)
        {
            _wallet = wallet;
            _upgradesRegistry = upgradesRegistry;
            _assetProvider = assetProvider;
            InstantiateUpgradeCards();
        }

        private void InstantiateUpgradeCards()
        {
            ContentUtility.ClearContentStorage(_contentStorage);
            
            foreach (var upgradable in _upgradesRegistry.Upgradeables)
            {
                _assetProvider.Instantiate<UpgradeCard>(WindowPaths.UpgradeCard, _contentStorage)
                    .ConstructDisplay(upgradable, upgradable.UpgradeInformation, OnUpgradePurchasing);
            }
        }
        private void OnUpgradePurchasing(IUpgradable upgradable)
        {
            if (upgradable.IsUpgradable)
            {
                _wallet.TakeMoney(upgradable.UpgradePrice, out bool isSucceeded);
            
                if (isSucceeded)
                    upgradable.Upgrade();
            
                InstantiateUpgradeCards();   
            }
        }

        protected override void Close()
        {
            base.Close();
            Destroy(gameObject);
        }

        protected override void OnEnabling()
        {
            base.OnEnabling();
            _closeButton.onClick.AddListener(Close);
        }
        
        protected override void OnDisabling()
        {
            base.OnDisabling();
            _closeButton.onClick.RemoveAllListeners();
        }
    }
}