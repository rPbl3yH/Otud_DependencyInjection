using UnityEngine;
using UnityEngine.Events;

namespace Game.Meta
{
    public class SceneInventory : MonoBehaviour
    {
        [SerializeField] private string _inventoryNameLocalizationKey;

        public Inventory Inventory => _inventory;
        public InventoryItemsList InventoryItemsList => _itemsList;
        
        [SerializeField] private InventoryItemsList _itemsList;
        [SerializeField] private Inventory _inventory;
        public UnityEvent OnOpenInventory => _onOpenInventory;
        [Space]
        [SerializeField] private UnityEvent _onOpenInventory;
        public UnityEvent OnCloseInventory => _onCloseInventory;
        [SerializeField] private UnityEvent _onCloseInventory;
        
        private void Start()
        {
            if (_itemsList != null)
            {
                _inventory.AddItems(_itemsList);
            }
        }
    }
}