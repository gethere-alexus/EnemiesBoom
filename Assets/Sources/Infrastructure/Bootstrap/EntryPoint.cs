using Sources.Infrastructure.GameMachine.States;
using Sources.Infrastructure.SceneLoad;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Bootstrap
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        private Game _gameInstance;

        private void Awake()
        {
            DiContainer container = FindObjectOfType<ProjectContext>().Container;
            
            _gameInstance = new Game(this, container);
            _gameInstance.GameStateMachine.Enter<BootstrapState>();
            
            DontDestroyOnLoad(this);
        }
    }
}