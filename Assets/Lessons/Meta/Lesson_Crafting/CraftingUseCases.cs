using Lessons.Meta.Lesson_Inventory;
using UnityEngine;

namespace Lessons.Meta.Lesson_Crafting
{
    public class CraftingUseCases
    {
        public static void Craft(Inventory inventory, ItemReceipt itemReceipt)
        {
            foreach (var ingredient in itemReceipt.Ingredients)
            {
                var count = InventoryUseCases.GetItemCount(inventory, ingredient.Config.Prototype.Clone());
                if (count < ingredient.Count)
                {
                    return;
                }
            }
            
            foreach (var ingredient in itemReceipt.Ingredients)
            {
                for (int i = 0; i < ingredient.Count; i++)
                {
                    InventoryUseCases.RemoveItem(inventory, ingredient.Config);
                }
            }
            
            InventoryUseCases.AddItem(inventory, itemReceipt.ResultItem);
        }

        public static InventoryItemConfig CreateItemConfig(string id)
        {
            var config = ScriptableObject.CreateInstance<InventoryItemConfig>();
            config.Prototype = new InventoryItem(id);
            return config;
        }
    }
}