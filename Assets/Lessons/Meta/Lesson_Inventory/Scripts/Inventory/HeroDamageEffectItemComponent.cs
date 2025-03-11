using System;

namespace Lessons.Meta.Lesson_Inventory
{
    [Serializable]
    public class HeroDamageEffectItemComponent : IItemComponent
    {
        public int DamageEffect = 2;
        
        public IItemComponent Clone()
        {
            return new HeroDamageEffectItemComponent()
            {
                DamageEffect = 2,
            };
        }
    }
}