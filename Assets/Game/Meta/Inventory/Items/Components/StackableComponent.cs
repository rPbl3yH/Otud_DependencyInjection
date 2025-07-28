using System;

namespace Game.Meta
{
    [Serializable]
    public class StackableComponent : IItemComponent
    {
        public bool IsFull => Count >= MaxCount;
        public int MaxCount;
        public int Count;
        
        public IItemComponent Clone()
        {
            return new StackableComponent()
            {
                MaxCount = MaxCount,
                Count = Count,
            };
        }
    }
}