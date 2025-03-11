namespace Lessons.Meta.Lesson_Inventory
{
    public class HeroItemsConsumeObserver
    {
        private Inventory _inventory;
        private Hero _hero;
        
        public void Construct(Inventory inventory, Hero hero)
        {
            _hero = hero;
            _inventory = inventory;
            _inventory.OnItemConsumed += OnItemConsumed;
        }

        public void OnDispose()
        {
            _inventory.OnItemAdded -= OnItemConsumed;
        }

        private void OnItemConsumed(InventoryItem item)
        {
            if (item.TryGetComponent(out HeroDamageEffectItemComponent effectItemComponent))
            {
                _hero.Damage += effectItemComponent.DamageEffect;
            }
        }
    }
}