using Sources.Infrastructure.SceneLoad;
using UnityEngine;

namespace Sources.Infrastructure.Bootstrap
{
    public class EntryPoint : MonoBehaviour, ICoroutineRunner
    {
        private Game _gameInstance;

        private void Awake()
        {
            if (_gameInstance == null)
            {
                _gameInstance = new Game(this);
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}