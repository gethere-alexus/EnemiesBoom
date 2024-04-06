using Infrastructure.Services.WindowProvider;
using UnityEngine;

namespace Sources.Windows
{
    public abstract class WindowBase : MonoBehaviour
    {
        public WindowType WindowType;
        public abstract void Close();
    }
}