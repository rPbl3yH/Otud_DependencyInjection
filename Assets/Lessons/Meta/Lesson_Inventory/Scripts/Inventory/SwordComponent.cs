using System;

namespace Lessons.Meta.Lesson_Inventory
{
    [Serializable]
    public class SwordComponent : IItemComponent
    {
        public int Damage;
        
        public IItemComponent Clone()
        {
            return new SwordComponent()
            {
                Damage = Damage,
            };
        }
    }
}