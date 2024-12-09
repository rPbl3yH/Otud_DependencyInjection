using System;

namespace Lessons.Meta.Lesson_Inventory
{
    public interface IItemComponent
    {
        IItemComponent Clone();
    }

    [Serializable]
    public class StackComponent : IItemComponent
    {
        public int Count = 0;
        public int MaxCount = 5;
        
        public IItemComponent Clone()
        {
            return new StackComponent()
            {
                Count = Count,
                MaxCount = MaxCount,
            };
        }
    }
}