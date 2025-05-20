namespace Lessons.Meta.Lesson_Inventory
{
    public class HeroItemsEffectsController
    {
        private Inventory _inventory;
        private Hero _hero;
        
        public void Construct(Inventory inventory, Hero hero)
        {
            _hero = hero;
            _inventory = inventory;
            _inventory.OnItemAdded += OnItemAdded;
            _inventory.OnItemRemoved += OnItemRemoved;
        }

        public void OnDispose()
        {
            _inventory.OnItemAdded -= OnItemAdded;
            _inventory.OnItemRemoved -= OnItemRemoved;
        }
        
        private void OnItemAdded(InventoryItem item)
        {
            if (HasEffect(item))
            {
                if (item.TryGetComponent(out HeroDamageEffectItemComponent effectItemComponent))
                {
                    _hero.Damage += effectItemComponent.DamageEffect;
                }
            }
        }

        private void OnItemRemoved(InventoryItem item)
        {
            if (HasEffect(item))
            {
                if (item.TryGetComponent(out HeroDamageEffectItemComponent effectItemComponent))
                {
                    _hero.Damage -= effectItemComponent.DamageEffect;
                }
            }
        }

        private bool HasEffect(InventoryItem item)
        {
            //Flags = 01100
            //Effectible = 01000
            //01100
            //&
            //01000
            //=
            //01000
            
            //Flags = 00100
            //Effectible = 01000
            //00100
            //&
            //01000
            //=
            //00000
            
            return (item.Flags & ItemFlags.Effectible) == ItemFlags.Effectible;
        }
    }
}