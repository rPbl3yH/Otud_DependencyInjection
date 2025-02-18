using System;
using Lessons.Meta.Lesson_Inventory;

namespace Lessons.Meta.Lesson_TDD
{
    [Serializable]
    public class ItemIngredient
    {
        public InventoryItemConfig ItemConfig;
        public int Count;
    }
}