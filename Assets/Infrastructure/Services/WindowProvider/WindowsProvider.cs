using System.Collections.Generic;
using Infrastructure.AssetsPaths;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.Factories.UI;
using Infrastructure.Services.PrefabLoad;
using Sources.Windows;
using UnityEngine;

namespace Infrastructure.Services.WindowProvider
{
    public class WindowsProvider : IWindowsProvider
    {
        private readonly IUIFactory _uifactory;
     
        private readonly Dictionary<WindowType, WindowBase> _windows;
        private readonly Dictionary<WindowType, WindowBase> _openedWindows = new();
        
        public WindowsProvider(IUIFactory uifactory,IPrefabLoader prefabLoader)
        {
            _uifactory = uifactory;
            _windows = new Dictionary<WindowType, WindowBase>()
            {
                { WindowType.ConnectionLostWindow, prefabLoader.LoadPrefab<ConnectionLostWindow>(WindowPaths.ConnectionLost)},
                { WindowType.HeroesInventory , prefabLoader.LoadPrefab<HeroesInventoryWindow>(WindowPaths.HeroesInventory)},
            };
        }

        public void OpenWindow(WindowType window)
        {
            CloseWindow(window);
            
            var windowInstance = Object.Instantiate(_windows[window], _uifactory.UIRoot.transform);
            _openedWindows.Add(window, windowInstance);
        }

        public void CloseWindow(WindowType window)
        {
            if (_openedWindows.ContainsKey(window))
            {
                WindowBase toClose = _openedWindows[window];
                
                if (toClose != null)
                    _openedWindows[window].Close();
                
                _openedWindows.Remove(window);
            }
        }
    }
}