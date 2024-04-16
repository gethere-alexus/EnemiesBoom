using Infrastructure.Curtain;
using Infrastructure.GameMachine;
using Infrastructure.GameMachine.States;
using Infrastructure.SceneLoad;
using Zenject;

namespace Infrastructure.Bootstrap
{
    /// <summary>
    /// Instance of the game, stores GameStateMachine which controls game life cycle.
    /// </summary>
    public class Game
    {
        public readonly GameStateMachine GameStateMachine;
        public readonly SceneLoader SceneLoader;

        public Game(ICoroutineRunner coroutineRunner, DiContainer container, LoadingCurtain loadingCurtain)
        {
            SceneLoader = new SceneLoader(coroutineRunner, loadingCurtain);
            GameStateMachine = new GameStateMachine(SceneLoader, container, coroutineRunner,loadingCurtain);
            GameStateMachine.Enter<BootstrapState>();
        }
    }
}