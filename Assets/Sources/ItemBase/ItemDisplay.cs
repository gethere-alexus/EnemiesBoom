using System.Collections.Generic;
using Sources.SlotBase;
using Sources.SlotsHolderBase;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.ItemBase
{
    /// <summary>
    /// Item view model
    /// </summary>
    public class ItemDisplay : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _levelText;
        private const int ClicksToMerge = 2; // double click

        private SlotsHolder _slotsHolder;
        private SlotDisplay _storingSlot;
        private Transform _draggingParent;

        private Item _itemInstance;

        public void Construct(Item displayingItem,SlotsHolder slotsHolder, SlotDisplay storedAt, Transform draggingParent)
        {
            _slotsHolder = slotsHolder;
            _storingSlot = storedAt;
            _draggingParent = draggingParent;
            _itemInstance = displayingItem;

            _itemInstance.LevelIncreased += UpdateDisplay;
            UpdateDisplay();
        }

        public void OnBeginDrag(PointerEventData eventData) => 
            gameObject.transform.SetParent(_draggingParent);

        public void OnDrag(PointerEventData eventData) => 
            gameObject.transform.position = Input.mousePosition;

        public void OnEndDrag(PointerEventData eventData) =>
           OnItemDragReleased(eventData);

        /// <summary>
        /// To track double clicking
        /// </summary>
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickCount == ClicksToMerge)
               OnDoubleClicked(eventData);
        }

        private void OnItemDragReleased(PointerEventData eventData)
        {
            ReturnItemSlotPosition();
            if (TryGetAnotherSlotDisplay(eventData, out SlotDisplay display))
            {
                display.SlotInstance.PutItem(_itemInstance, out bool isSucceeded);

                if (isSucceeded)
                    _storingSlot.SlotInstance.RemoveStoringItem();
            }
        }

        /// <param name="data">Pointer data</param>
        private void OnDoubleClicked(PointerEventData data)
        {
            if(TryGetSlotDisplay(data, out SlotDisplay display))
                _slotsHolder.TryMergeSlotsItems(_storingSlot.SlotInstance);
        }

        /// <summary>
        /// Searches for slot display where player clicked, not including where this item stored
        /// </summary>
        private bool TryGetAnotherSlotDisplay(PointerEventData eventData, out SlotDisplay display)
        {
            bool operationResult = false;
            display = null;
            
            foreach (var result in RaycastPoint(eventData))
            {
                if (result.gameObject.TryGetComponent(out SlotDisplay slot) && slot != _storingSlot)
                {
                    display = slot;
                    operationResult = true;
                }
            }

            return operationResult;
        }

        /// <summary>
        /// Searches for slot display where player clicked, including where this item stored
        /// </summary>
        private bool TryGetSlotDisplay(PointerEventData eventData, out SlotDisplay display)
        {
            bool operationResult = false;
            display = null;
            
            foreach (var result in RaycastPoint(eventData))
            {
                if (result.gameObject.TryGetComponent(out SlotDisplay slot))
                {
                    display = slot;
                    operationResult = true;
                }
            }

            return operationResult;
        }

        /// <summary>
        /// Returns item position to where it is stored
        /// </summary>
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

        private void UpdateDisplay() => 
            _levelText.text = _itemInstance.Level.ToString();
    }
}