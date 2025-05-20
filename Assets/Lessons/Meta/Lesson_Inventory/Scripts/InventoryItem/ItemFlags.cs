using System;

namespace Lessons.Meta.Lesson_Inventory
{
    [Flags]
    public enum ItemFlags
    {
        None = 0, //00000
        Stackable = 1, //00001
        Consumable = 2, //00010
        Equippable = 4, //00100
        Effectible = 8, //01000
        
        //01100
    }
}