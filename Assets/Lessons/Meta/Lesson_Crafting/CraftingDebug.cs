using Lessons.Meta.Lesson_Inventory;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Meta.Lesson_Crafting
{
    public class CraftingDebug : MonoBehaviour
    {
        public Inventory Inventory;

        [Button]
        public void AddItem(InventoryItemConfig itemConfig)
        {
            InventoryUseCases.AddItem(Inventory, itemConfig);
        }

        [Button]
        public void Craft(ItemReceipt itemReceipt)
        {
            CraftingUseCases.Craft(Inventory, itemReceipt);
        }
    }
}