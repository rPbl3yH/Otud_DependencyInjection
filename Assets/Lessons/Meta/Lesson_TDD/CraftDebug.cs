using Lessons.Meta.Lesson_Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Meta.Lesson_TDD
{
    public class CraftDebug : MonoBehaviour
    {
        public InventoryDebug InventoryDebug;
        
        [Button]
        public void Craft(ItemReceipt itemReceipt)
        {
            CraftUseCases.CraftItem(InventoryDebug.Inventory, itemReceipt);
        }
    }
}