using System.Collections.Generic;
using Infrastructure.Services.Factories.Windows;
using Sources.Windows;
using UnityEngine;
using Zenject;

namespace Infrastructure.Services.WindowProvider
{
    public class WindowsProvider : IWindowsProvider
    {
        
        private readonly Dictionary<WindowType, WindowBase> _openedWindows = new();
        private readonly IWindowsFactory _windowsFactory;
        private int _windowSortOrder = 5;

        [Inject]
        public WindowsProvider(IWindowsFactory windowsFactory) => 
            _windowsFactory = windowsFactory;

        public void OpenWindow(WindowType window)
        {
            CloseWindow(window);
            
            var windowInstance = _windowsFactory.CreateWindow(window, _windowSortOrder, () => CloseWindow(window));
            _openedWindows.Add(window, windowInstance);
            
            IncreaseWindowSortOrder();
        }

        public void OpenWindow<TPayload>(WindowType window, TPayload payload)
        {
            CloseWindow(window);
            
            var windowInstance = _windowsFactory.CreateWindow<TPayload>(window, _windowSortOrder, payload, () => CloseWindow(window));
            _openedWindows.Add(window, windowInstance);
            
            IncreaseWindowSortOrder();
        }

        public void CloseWindow(WindowType window)
        {
            if (_openedWindows.ContainsKey(window))
            {
                WindowBase toClose = _openedWindows[window];

                if (toClose != null)
                    Object.Destroy(toClose);
                
                DecreaseWindowSortOrder();
                _openedWindows.Remove(window);
            }
        }

        private void IncreaseWindowSortOrder() => 
            _windowSortOrder++;

        private void DecreaseWindowSortOrder() => 
            _windowSortOrder--;
    }
}