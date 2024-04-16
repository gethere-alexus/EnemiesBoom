using System;
using UnityEngine;

namespace Sources.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        private event Action WindowClosing;
        public void SubscribeActions(Action onClosed)
        {
            WindowClosing += onClosed;
        }
        protected virtual void OnAwake()
        {
            
        }

        protected virtual void OnDestroying()
        {
            
        }

        protected virtual void OnDisabling()
        {
            
        }

        protected virtual void Close() => 
            WindowClosing?.Invoke();

        private void Awake() 
            => OnAwake();

        private void OnDisable() => 
            OnDisabling();

        private void OnDestroy() => 
            OnDestroying();
    }
}