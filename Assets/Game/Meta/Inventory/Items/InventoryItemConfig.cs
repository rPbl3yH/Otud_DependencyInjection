using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Meta
{
    [CreateAssetMenu(
        fileName = "ItemConfig",
        menuName = "Inventory/New ItemConfig"
    )]
    public class InventoryItemConfig : ScriptableObject
    {
        public InventoryItem Item;

        public InventoryItem InstantiateItem()
        {
            return Item.Clone();
        }
    }
}