using Lessons.Meta.Lesson_Inventory;

namespace Lessons.Meta.Lesson_TDD
{
    public class CraftUseCases
    {
        public static void CraftItem(Inventory inventory, ItemReceipt itemReceipt)
        {
            foreach (var ingredient in itemReceipt.Ingredients)
            {
                var ingredientName = ingredient.ItemConfig.Prototype.Name;
                bool canCraft = inventory.HasItems(ingredientName, ingredient.Count);

                if (!canCraft)
                {
                    return;
                }
            }

            foreach (var ingredient in itemReceipt.Ingredients)
            {
                var prototype = ingredient.ItemConfig.Prototype;
                inventory.RemoveItems(prototype, ingredient.Count);
            }
            
            inventory.AddItem(itemReceipt.ResultItemConfig.Prototype);
        }
    }
}