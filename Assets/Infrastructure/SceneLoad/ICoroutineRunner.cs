using System.Collections;
using UnityEngine;

namespace Infrastructure.SceneLoad
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(IEnumerator coroutine);
    }
}