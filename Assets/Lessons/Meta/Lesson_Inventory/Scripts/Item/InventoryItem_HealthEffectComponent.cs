using System;

namespace Lessons.Meta.Lesson_Inventory
{
    [Serializable]
    public class InventoryItem_ManaEffectComponent : IItemComponent
    {
        public float ManaValue;
        
        public IItemComponent Clone()
        {
            return new InventoryItem_ManaEffectComponent()
            {
                ManaValue = ManaValue,
            };
        }
    }

    [Serializable]
    public class InventoryItem_HealthEffectComponent : IItemComponent, IEffectibleComponent
    {
        public int HitPoints = 2;
        
        public IItemComponent Clone()
        {
            return new InventoryItem_HealthEffectComponent()
            {
                HitPoints = HitPoints,
            };
        }

        public void Apply()
        {
            Hero.Instance.MaxHitPoints += HitPoints;
        }

        public void Discard()
        {
            Hero.Instance.MaxHitPoints -= HitPoints;
        }
    }
}