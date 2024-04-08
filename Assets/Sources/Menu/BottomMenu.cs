using Infrastructure.Services.WindowProvider;
using UnityEngine;

namespace Sources.Menu
{
    public class BottomMenu : MonoBehaviour
    {
        [SerializeField] private MenuSection[] _menuSections;

        public void Construct(IWindowsProvider windowsProvider)
        {
            foreach (var menuSection in _menuSections)
            {
                menuSection.InteractionButton.onClick.AddListener(() => 
                    windowsProvider.OpenWindow(menuSection.OnClickWindow));
            }
        }

        private void OnDisable()
        {
            foreach (var menuSection in _menuSections)
            {
                menuSection.InteractionButton.onClick.RemoveAllListeners();
            }
        }
    }
}