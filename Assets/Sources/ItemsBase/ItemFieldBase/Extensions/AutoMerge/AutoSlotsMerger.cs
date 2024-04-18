using System.Collections;
using Infrastructure.ProgressData;
using Infrastructure.Services.AutoProcessesControl.Connection;
using Infrastructure.Services.ProgressLoad.Connection;
using Sources.ItemsBase.ItemSlotBase;
using UnityEngine;

namespace Sources.ItemsBase.ItemFieldBase.Extensions.AutoMerge
{
    /// <summary>
    /// Automatically merges the items, placed on a grid.
    /// Parametrized by AutoMergerConfig.
    /// </summary>
    public class AutoSlotsMerger : MonoBehaviour, IAutoProcessController, IProgressWriter
    {
        private ItemField _itemField;

        private float _usageCoolDown;
        private const float MinCoolDown = 0.1f;
        public void Construct(ItemField itemField)
        {
            _itemField = itemField;
            _itemField.SlotsMerged += RestartProcess;
        }

        public void DecreaseMergeCoolDown(float decreaseBy)
        {
            StopProcess();
            
            _usageCoolDown -= decreaseBy;
            
            if (_usageCoolDown < MinCoolDown)
                _usageCoolDown = MinCoolDown;
            
            StartProcess();
        }

        public void StartProcess() => 
            StartCoroutine(MergeAutomatically());

        public void RestartProcess()
        {
            StopAllCoroutines();
            StartCoroutine(MergeAutomatically());
        }

        public void StopProcess() => 
            StopAllCoroutines();

        public void LoadProgress(GameProgress progress) => 
            _usageCoolDown = progress.FieldExtensions.SlotsAutoMerger.UsageCoolDown;

        public void SaveProgress(GameProgress progress) =>
            progress.FieldExtensions.SlotsAutoMerger.UsageCoolDown = _usageCoolDown;

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
            for (i = 0; i < _itemField.Grid.Length - 1; i++)
            {
                for (j = i + 1; j < _itemField.Grid.Length; j++)
                {
                    ItemSlot a = _itemField.Grid[i], b = _itemField.Grid[j];
                        
                    if (!mergingSlotsFound && a.StoringItem != null && b.StoringItem != null)
                    {
                        if (a.StoringItem.Level == b.StoringItem.Level)
                        {
                            _itemField.TryMergeItems(a,b);
                            mergingSlotsFound = true;
                        }
                    }
                }
            }
        }

        private void OnDisable()
        {
            _itemField.SlotsMerged -= RestartProcess;
            StopProcess();
        }
    }
}