using Sources.AnvilBase;
using Sources.AnvilBase.AnvilExtensions.AutoRefiller;
using Sources.AnvilBase.AnvilExtensions.AutoUse;
using Sources.AnvilBase.AnvilExtensions.ChargesRefiller;
using UnityEngine;

namespace Sources.ItemsBase.ItemFieldBase
{
    public class FieldControl : MonoBehaviour
    {
        [SerializeField] private AnvilAutoUse _anvilAutoUse;
        [SerializeField] private AnvilAutoRefiller _anvilAutoRefiller;
        [SerializeField] private AnvilChargesRefillDisplay _anvilChargesRefillDisplay;
        [SerializeField] private AnvilDisplay _anvilDisplay;

        public AnvilAutoUse AnvilAutoUseInstance => _anvilAutoUse;
        public AnvilAutoRefiller AnvilAutoRefiller => _anvilAutoRefiller;
        public AnvilChargesRefillDisplay AnvilChargesRefillDisplay => _anvilChargesRefillDisplay;
        public AnvilDisplay AnvilDisplay => _anvilDisplay;
    }
}