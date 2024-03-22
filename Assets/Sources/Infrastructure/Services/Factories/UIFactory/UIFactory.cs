using UnityEngine;

namespace Sources.Infrastructure.Services.Factories.UIFactory
{
    public class UIFactory : IUIFactory
    {
        public UIFactory()
        {
            Debug.Log("Factory created");
        }

        public void CreateUIRoot()
        {
            
        }
    }
}