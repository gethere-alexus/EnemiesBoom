using System.Collections;
using UnityEngine;

namespace Sources.Infrastructure.SceneLoad
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}