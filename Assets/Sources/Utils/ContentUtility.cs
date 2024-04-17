using UnityEngine;

namespace Sources.Utils
{
    public static class ContentUtility
    {
        public static void ClearContentStorage(Transform storage)
        {
            foreach (Transform child in storage)
            {
                Object.Destroy(child.gameObject);
            }
        }
    }
}