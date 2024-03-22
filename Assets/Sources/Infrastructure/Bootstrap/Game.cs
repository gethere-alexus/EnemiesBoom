using Sources.Infrastructure.Curtain;
using Sources.Infrastructure.GameMachine;
using Sources.Infrastructure.GameMachine.States;
using Sources.Infrastructure.SceneLoad;
using Zenject;

namespace Sources.Infrastructure.Bootstrap
{
    public class Game
    {
        public readonly GameStateMachine GameStateMachine;
        public readonly SceneLoader SceneLoader;

        public Game(ICoroutineRunner coroutineRunner, DiContainer container, LoadingCurtain loadingCurtain)
        {
            SceneLoader = new SceneLoader(coroutineRunner, loadingCurtain);
            GameStateMachine = new GameStateMachine(SceneLoader, container, loadingCurtain);
        }
    }
}