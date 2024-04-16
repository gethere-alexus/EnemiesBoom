using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.Utils
{
    public static class Raycaster
    {
        public static RaycastResult[] RaycastPoint(PointerEventData eventData)
        {
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);
            return raycastResults.ToArray();
        }
        
        public static bool TryGetComponent<TComponent>(PointerEventData eventData, out TComponent component, TComponent exclude = null) where TComponent : class
        {
            bool operationResult = false;
            component = null;
            
            foreach (var result in RaycastPoint(eventData))
            {
                bool isFound = result.gameObject.TryGetComponent(out TComponent searchResult);
                if (isFound && searchResult != exclude)
                {
                    component = searchResult;
                    operationResult = true;
                    
                }
            }
            return operationResult;
        }
    }
}