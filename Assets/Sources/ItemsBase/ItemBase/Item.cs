using System;

namespace Sources.ItemsBase.ItemBase
{
    /// <summary>
    /// An item, can be stored in slots
    /// </summary>
    public class Item
    {
        private const int MinItemLevel = 1;
        private int _level;
        public event Action LevelChanged;

        public Item(int itemLevel)
        {
            if (itemLevel < MinItemLevel)
                itemLevel = MinItemLevel;

            _level = itemLevel;
        }

        public Item()
        {
            _level = MinItemLevel;
        }

        /// <summary>
        /// Upgrades an item
        /// </summary>
        public void Upgrade() =>
            IncreaseLevel();
        public void Downgrade() => 
            DecreaseLevel();

        public void SetLevel(int toSet)
        {
            if (toSet < MinItemLevel)
                toSet = MinItemLevel;
            _level = toSet;
            LevelChanged?.Invoke();
        }

        public void SetDefaultLevel()
        {
            _level = MinItemLevel;
            LevelChanged?.Invoke();
        }

        private void DecreaseLevel()
        {
            _level--;
            LevelChanged?.Invoke();
        }

        /// <summary>
        /// Increases item level by one
        /// </summary>
        private void IncreaseLevel()
        {
            _level++;
            LevelChanged?.Invoke();
        }

        public int Level => _level;
        public bool IsDefaultItemLevel => _level == MinItemLevel;
    }
}