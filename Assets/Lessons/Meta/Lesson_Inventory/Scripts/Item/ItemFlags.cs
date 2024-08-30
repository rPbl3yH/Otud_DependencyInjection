using System;

namespace Lessons.Meta.Lesson_Inventory
{
    [Flags]
    public enum ItemFlags
    {
        None = 0,
        Consumable = 1,
        Stackable = 2,
        Effectible = 4,
        //8
        //16
        //32
        
        //0011
        //0001 + 0010 = 0011
        //0000 + 0001 = 0000
        //0000, 0001, 0010, 0100
    }
}