using System.Collections.Generic;
using Sources.SlotBase;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.ItemBase
{
    public class ItemDisplay : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        [SerializeField] private TMP_Text _levelText;
        
        private SlotDisplay _storingSlot;
        private Transform _draggingParent;
        
        private Item _itemInstance;

        public void Construct(Item displayingItem, SlotDisplay storedAt, Transform draggingParent)
        {
            _storingSlot = storedAt;
            _draggingParent = draggingParent;
            _itemInstance = displayingItem;
            
            _itemInstance.LevelIncreased += UpdateDisplay;
            UpdateDisplay();
        }

        private void UpdateDisplay()
        {
            _levelText.text = _itemInstance.Level.ToString();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            gameObject.transform.SetParent(_draggingParent);
        }

        public void OnDrag(PointerEventData eventData)
        {
            gameObject.transform.position = Input.mousePosition;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            ReturnItemSlotPosition();
            
            foreach (var result in RaycastPoint(eventData))
            {
                if (result.gameObject.TryGetComponent(out SlotDisplay slot) && slot != _storingSlot)
                {
                    slot.SlotInstance.PutItem(_itemInstance, out bool isSucceeded);
                    
                    if (isSucceeded)
                        _storingSlot.SlotInstance.RemoveStoringItem();
                }
            }
        }

        private void ReturnItemSlotPosition()
        {
            Transform itemTransform = transform;
            
            itemTransform.SetParent(_storingSlot.transform);
            itemTransform.position = _storingSlot.transform.position;
        }

        private RaycastResult[] RaycastPoint(PointerEventData eventData)
        {
            List<RaycastResult> raycastResults = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, raycastResults);
            return raycastResults.ToArray();
        }
    }
}