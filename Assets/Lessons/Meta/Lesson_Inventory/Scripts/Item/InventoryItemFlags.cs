using System;

namespace Lessons.Meta.Lesson_Inventory
{
    [Flags]
    public enum InventoryItemFlags
    {
        None = 0,
        Effectible = 1,
        Consumable = 2,
        Stackable = 4,
        Equipable = 8,
    }
}