using UnityEngine;

namespace Game.Meta
{
    [CreateAssetMenu(
        fileName = "Items List",
        menuName = "Inventory/Items List"
    )]
    public class InventoryItemsList : ScriptableObject
    {
        public Loot[] Loots;
    }
}