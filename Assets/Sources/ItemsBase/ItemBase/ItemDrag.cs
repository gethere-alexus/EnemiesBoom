using System.Collections.Generic;
using Sources.ItemsBase.ItemSlotBase;
using Sources.Utils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.ItemsBase.ItemBase
{
    public class ItemDrag : MonoBehaviour
    {
        private readonly List<ItemInput> _subscribedInputs = new();
        
        public void SubscribeInput(ItemInput input)
        {
            input.OnDragBegun += StartDrag;
            input.OnDragging += Drag;
            input.OnDragged += OnDragged;
            
            _subscribedInputs.Add(input);
        }

        private void StartDrag(ItemDisplay itemDisplay) =>
            itemDisplay.gameObject.transform.SetParent(transform);

        private void Drag(ItemDisplay itemDisplay) =>
            itemDisplay.gameObject.transform.position = Input.mousePosition;

        private void OnDragged(ItemDisplay itemDisplay, IItemStorage storingItemSlot, PointerEventData eventData)
        {
            SetItemPosition(itemDisplay.transform, returnTo: storingItemSlot.Storage);
            
            if (Raycaster.TryGetComponent<IItemStorage>(eventData, out IItemStorage storage, storingItemSlot))
            {
                storage.Store(itemDisplay.ItemInstance, storingItemSlot);
            }
        }
        private void SetItemPosition(Transform itemDisplayTransform, Transform returnTo)
        {
            itemDisplayTransform.SetParent(returnTo);
            itemDisplayTransform.position = returnTo.transform.position;
        }

        private void OnDisable()
        {
            foreach (var subscribedDisplay in _subscribedInputs)
            {
                subscribedDisplay.OnDragBegun -= StartDrag;
                subscribedDisplay.OnDragging -= Drag;
                subscribedDisplay.OnDragged -= OnDragged;
            }

            _subscribedInputs.Clear();
        }
    }
}