using UnityEngine;

namespace Lessons.Meta.Lesson_Inventory
{
    [CreateAssetMenu(
        fileName = "InventoryItemConfig",
        menuName = "Inventory/New InventoryItemConfig"
    )]
    public class InventoryItemConfig : ScriptableObject
    {
        public InventoryItem Item;
    }
}