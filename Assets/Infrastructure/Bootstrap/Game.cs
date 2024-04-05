using Infrastructure.Curtain;
using Infrastructure.GameMachine;
using Infrastructure.SceneLoad;
using Zenject;

namespace Infrastructure.Bootstrap
{
    public class Game
    {
        public readonly GameStateMachine GameStateMachine;
        public readonly SceneLoader SceneLoader;

        public Game(ICoroutineRunner coroutineRunner, DiContainer container, LoadingCurtain loadingCurtain)
        {
            SceneLoader = new SceneLoader(coroutineRunner, loadingCurtain);
            GameStateMachine = new GameStateMachine(SceneLoader, container, coroutineRunner,loadingCurtain);
        }
    }
}