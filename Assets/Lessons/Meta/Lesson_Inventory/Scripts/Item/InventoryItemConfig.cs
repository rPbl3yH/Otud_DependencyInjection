using UnityEngine;

namespace Lessons.Meta.Lesson_Inventory
{
    [CreateAssetMenu(
        fileName = "InventoryItemConfig",
        menuName = "Lessons/Configs/New InventoryItemConfig"
    )]
    public class InventoryItemConfig : ScriptableObject
    {
        public InventoryItem Prototype;
    }
}