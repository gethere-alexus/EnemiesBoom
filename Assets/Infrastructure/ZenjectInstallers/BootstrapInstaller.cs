using Infrastructure.Bootstrap;
using Infrastructure.Curtain;
using Infrastructure.SceneLoad;
using UnityEngine;
using Zenject;

namespace Infrastructure.ZenjectInstallers
{
    public class BootstrapInstaller : MonoInstaller, ICoroutineRunner, IInitializable
    {
        [SerializeField] private LoadingCurtain _loadingCurtain;

        public override void InstallBindings() => 
            Container.BindInterfacesTo<BootstrapInstaller>().FromInstance(this);

        public void Initialize()
        {
            InstallGameInstance();
        }

        private void InstallGameInstance()
        {
            Game gameInstance = new Game(this, Container, Instantiate(_loadingCurtain));
            Container.Bind<Game>().FromInstance(gameInstance).AsSingle();
        }
    }
}