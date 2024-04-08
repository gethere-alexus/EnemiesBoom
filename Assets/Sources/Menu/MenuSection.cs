using System;
using Infrastructure.Services.WindowProvider;
using UnityEngine.UI;

namespace Sources.Menu
{
    [Serializable]
    public class MenuSection
    {
        public Button InteractionButton;
        public WindowType OnClickWindow;
    }
}