using Infrastructure.Paths;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.ProgressLoad;
using Sources.SlotBase;
using UnityEngine;

namespace Sources.GameFieldBase
{
    /// <summary>
    /// View model of game field, the main purpose is to build a grid and initialize the model.
    /// </summary>
    public class GameFieldDisplay : MonoBehaviour
    {
        
        [SerializeField, Tooltip("Where all the slots will be stored")]
        private Transform _slotStorage;

        [SerializeField, Tooltip("Transform for an item when it is being dragged")]
        private Transform _itemDraggingParent;
        
        private GameField _gameFieldInstance;
        private const int GridSlots = 40;
        public void Construct(IAssetProvider assetProvider, IProgressProvider progressProvider)
        {
            _gameFieldInstance = new GameField(progressProvider);
            InstantiateGrid(assetProvider, GridSlots);
        }

        private void InstantiateGrid(IAssetProvider assetProvider, int gridSlots)
        {
            ClearStorage();
            for (int i = 0; i < gridSlots; i++)
            {
                SlotDisplay slotDisplay = assetProvider.Instantiate<SlotDisplay>(AssetPaths.SlotTemplate, _slotStorage);
                slotDisplay.Construct(assetProvider, _gameFieldInstance, _itemDraggingParent);
                
                _gameFieldInstance.Grid[i] = slotDisplay.SlotInstance;
            }
        }

        /// <summary>
        /// Clears grid storage if there is some game objects.
        /// </summary>
        private void ClearStorage()
        {
            // Clears the slots storage if there is any existing slots
            if (_slotStorage.childCount != 0)
            {
                foreach (Transform child in _slotStorage)
                    Destroy(child.gameObject);
            }
        }

        public GameField GameFieldInstance => _gameFieldInstance;
    }
}