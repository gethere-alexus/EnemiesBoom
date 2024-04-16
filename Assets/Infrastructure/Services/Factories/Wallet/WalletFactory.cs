using Infrastructure.PrefabPaths;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.Factories.UI;
using Infrastructure.Services.ProgressLoad;
using Sources.WalletBase;
using Zenject;

namespace Infrastructure.Services.Factories.Wallet
{
    public class WalletFactory : IWalletFactory
    {
        private readonly IUIRootFactory _uiRootFactory;
        private readonly IAssetProvider _assetProvider;
        private readonly IProgressProvider _progressProvider;
        private readonly DiContainer _instanceRegistry;

        private IWallet _walletInstance;
        
        public WalletFactory(IUIRootFactory uiRootFactory,  IAssetProvider assetProvider, IProgressProvider progressProvider, DiContainer instanceRegistry)
        {
            _uiRootFactory = uiRootFactory;
            _assetProvider = assetProvider;
            _progressProvider = progressProvider;
            _instanceRegistry = instanceRegistry;
        }

        public void CreateWalletDisplay()
        {
            _walletInstance ??= _instanceRegistry.Resolve<IWallet>();
            var display = _assetProvider.Instantiate<WalletDisplay>(WalletPaths.WalletDisplay, _uiRootFactory.UIRoot.transform);
            
            display.Construct(_walletInstance);
        }

        private IWallet InstantiateWallet()
        {
            Sources.WalletBase.Wallet wallet = new Sources.WalletBase.Wallet();
            
            _progressProvider.RegisterObserver(wallet);
            _instanceRegistry.Bind<IWallet>().FromInstance(wallet).AsSingle();

            return wallet;
        }
    }
}