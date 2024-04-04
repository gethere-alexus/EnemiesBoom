using UnityEngine;

namespace Infrastructure.Services.WindowProvider
{
    public abstract class WindowBase : MonoBehaviour
    {
        public WindowType WindowType;
        public abstract void Close();
    }
}