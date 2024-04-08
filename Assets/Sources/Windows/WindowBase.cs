using System;
using Infrastructure.Services.WindowProvider;
using UnityEngine;

namespace Sources.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public WindowType Window;
        protected abstract void OnAwake();
        protected abstract void OnDestroying();
        public abstract void Close();

        private void Awake() 
            => OnAwake();

        private void OnDestroy() => 
            OnDestroying();
    }
}