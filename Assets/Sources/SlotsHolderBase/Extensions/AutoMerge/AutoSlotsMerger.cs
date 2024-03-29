using System;
using System.Collections;
using Infrastructure.Configurations.SlotsField;
using Sources.SlotBase;
using UnityEngine;

namespace Sources.SlotsHolderBase.Extensions.AutoMerge
{
    /// <summary>
    /// Automatically merges the items, placed on a grid.
    /// Parametrized by AutoMergerConfig.
    /// </summary>
    public class AutoSlotsMerger : MonoBehaviour
    {
        private SlotsHolder _slotsHolder;
        private float _usageCoolDown;
        
        /// <param name="config">Auto Merger Config</param>
        public void Construct(SlotsHolder slotsHolder, AutoMergerConfig config)
        {
            _slotsHolder = slotsHolder;
            _usageCoolDown = config.UsageCoolDown;
            _slotsHolder.SlotsMerged += RestartAutoMerge;
            StartCoroutine(MergeAutomatically());
        }

        /// <summary>
        /// Restart auto-merging cooldown
        /// </summary>
        private void RestartAutoMerge()
        {
            StopAllCoroutines();
            StartCoroutine(MergeAutomatically());
        }
        
        /// <summary>
        /// Merge items from a field with parametrized cooldown
        /// </summary>
        private IEnumerator MergeAutomatically()
        {
            while (true)
            {
                yield return new WaitForSeconds(_usageCoolDown);
                MergeItems();
            }
        }

        /// <summary>
        /// Looks for two slots, which store items with the same levels, and merge it.
        /// </summary>
        private void MergeItems()
        {
            int i, j;
            bool mergingSlotsFound = false;
            for (i = 0; i < _slotsHolder.Grid.Length - 1; i++)
            {
                for (j = i + 1; j < _slotsHolder.Grid.Length; j++)
                {
                    Slot a = _slotsHolder.Grid[i], b = _slotsHolder.Grid[j];
                        
                    if (!mergingSlotsFound && a.StoringItem != null && b.StoringItem != null)
                    {
                        if (a.StoringItem.Level == b.StoringItem.Level)
                        {
                            _slotsHolder.TryMergeSlotsItems(a,b);
                            mergingSlotsFound = true;
                        }
                    }
                }
            }
        }

        private void OnDisable()
        {
            _slotsHolder.SlotsMerged -= RestartAutoMerge;
            StopAllCoroutines();
        }
    }
}