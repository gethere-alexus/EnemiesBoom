using System;
using System.Collections.Generic;
using Infrastructure.Factories.UI;
using Infrastructure.PrefabPaths;
using Infrastructure.Services.PrefabLoad;
using Infrastructure.Services.WindowProvider;
using Sources.Windows;
using Sources.Windows.ConnectionLost;
using Sources.Windows.HeroesInventory;
using Sources.Windows.HeroesInventory.ActiveSlotSelection;
using Sources.Windows.Upgrades;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories.Windows
{
    public class WindowsFactory : IWindowsFactory
    {
        private readonly IUIRootFactory _uiFactory;
        private readonly DiContainer _container;
        private readonly Dictionary<WindowType, WindowBase> _windows;

        public WindowsFactory(IUIRootFactory uiFactory, IPrefabLoader prefabLoader, DiContainer container)
        {
            _uiFactory = uiFactory;
            _container = container;

            _windows = new Dictionary<WindowType, WindowBase>()
            {
                {
                    WindowType.ConnectionLost, prefabLoader.LoadPrefab<ConnectionLostWindow>(WindowPaths.ConnectionLost)
                },
                {
                    WindowType.HeroesInventory,
                    prefabLoader.LoadPrefab<HeroesInventoryWindow>(WindowPaths.HeroesInventory)
                },
                {
                    WindowType.SelectHeroSlot,
                    prefabLoader.LoadPrefab<SelectHeroSlotWindow>(WindowPaths.SelectActiveHeroSlot)
                },
                {
                  WindowType.UpgradesShop,
                  prefabLoader.LoadPrefab<UpgradesWindow>(WindowPaths.UpgradesShop)
                },
            };
        }

        public WindowBase CreateWindow(WindowType window, int windowOrder, Action onClosing)
        {
            GameObject instance = _container.InstantiatePrefab(_windows[window], _uiFactory.UIRoot.transform);

            Canvas instanceCanvas = instance.GetComponent<Canvas>();
            instanceCanvas.overrideSorting = true;
            instanceCanvas.sortingOrder = windowOrder;

            WindowBase instanceBase = instance.GetComponent<WindowBase>();
            instanceBase.SubscribeActions(onClosing);

            return instance.GetComponent<WindowBase>();
        }

        public WindowBase CreateWindow<TPayload>(WindowType window, int windowOrder, TPayload payload,
            Action onClosing = null)
        {
            WindowBase instance = CreateWindow(window, windowOrder, onClosing);

            if (instance is IPayloadWindow<TPayload> payloadWindow)
                payloadWindow.ConstructPayload(payload);

            return instance;
        }
    }
}