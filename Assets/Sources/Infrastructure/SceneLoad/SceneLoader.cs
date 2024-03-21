using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Infrastructure.SceneLoad
{
    public class SceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }

        public void Load(int sceneIndex, Action sceneLoaded = null) =>
            _coroutineRunner.StartCoroutine(LoadScene(sceneIndex, sceneLoaded));

        private IEnumerator LoadScene(int sceneIndex, Action sceneLoaded = null)
        {
            if (sceneIndex == SceneManager.GetActiveScene().buildIndex)
            {
                sceneLoaded?.Invoke();
                yield break;
            }
            
            AsyncOperation sceneAsync = SceneManager.LoadSceneAsync(sceneIndex);

            while (!sceneAsync.isDone)
                yield return null;

            sceneLoaded?.Invoke();
        }
    }
}