using System;
using Lessons.Meta.Lesson_Inventory;

namespace Lessons.Meta.Lesson_Crafting
{
    [Serializable]
    public class ReceiptIngredient
    {
        public InventoryItemConfig Config;
        public int Count;
    }
}