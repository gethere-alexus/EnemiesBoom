using Infrastructure.Services.ConfigLoad;
using Infrastructure.Services.ProgressLoad;
using Sources.HeroBase;
using Sources.WalletBase;
using Zenject;

namespace Infrastructure.ZenjectInstallers
{
    public class WalletInstaller : MonoInstaller, IInitializable
    {
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.BindInterfacesTo<WalletInstaller>().FromInstance(this).AsSingle();
        }
        
        public void Initialize()
        {
            IWallet wallet = new Wallet();
            
            Container.Resolve<IProgressProvider>().RegisterObserver(wallet);
            
            Container.Bind<IWallet>().FromInstance(wallet).AsSingle();
        }
    }
}