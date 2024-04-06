using System.Collections.Generic;
using Infrastructure.Paths;
using Infrastructure.Services.PrefabLoad;
using Sources.Windows;
using UnityEngine;

namespace Infrastructure.Services.WindowProvider
{
    public class WindowsProvider : IWindowsProvider
    {
        private readonly Dictionary<WindowType, WindowBase> _windows;
        private readonly Dictionary<WindowType, WindowBase> _openedWindows = new Dictionary<WindowType, WindowBase>();
        
        public WindowsProvider(IPrefabLoader prefabLoader)
        {
            _windows = new Dictionary<WindowType, WindowBase>()
            {
                { WindowType.ConnectionLostWindow, prefabLoader.LoadPrefab<ConnectionLostWindow>(WindowPaths.ConnectionLost)},
            };
        }

        public void OpenWindow(WindowType window)
        {
            CloseWindow(window);
            
            var windowInstance = Object.Instantiate(_windows[window]);
            _openedWindows.Add(window, windowInstance);
        }

        public void CloseWindow(WindowType window)
        {
            if (_openedWindows.ContainsKey(window))
            {
                _openedWindows[window].Close();
                _openedWindows.Remove(window);
            }
        }
    }
}