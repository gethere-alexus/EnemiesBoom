using System;
using Sources.ItemsBase.ItemSlotBase;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sources.ItemsBase.ItemBase
{
    [RequireComponent(typeof(ItemDisplay))]
    public class ItemInput : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
    {
        [SerializeField] private ItemDisplay _controlInputFor;
        [SerializeField] private int _clicksToMerge = 2;

        public event Action ItemMerging;
        public event Action<ItemDisplay> OnDragBegun, OnDragging;
        public event Action<ItemDisplay, ItemSlotDisplay, PointerEventData> OnDragged;

        private void Awake() => 
            _controlInputFor ??= GetComponent<ItemDisplay>();

        public void OnBeginDrag(PointerEventData eventData) =>
            OnDragBegun?.Invoke(_controlInputFor);

        public void OnDrag(PointerEventData eventData) =>
            OnDragging?.Invoke(_controlInputFor);

        public void OnEndDrag(PointerEventData eventData) =>
            OnDragged?.Invoke(_controlInputFor, _controlInputFor.StoredAt, eventData);
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.clickCount == _clicksToMerge)
                ItemMerging?.Invoke();
        }
    }
}