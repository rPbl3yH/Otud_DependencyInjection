namespace Lessons.Meta.Lesson_Inventory
{
    public class InventoryManaComponentObserver
    {
        private readonly Inventory _inventory;
        private readonly Hero _hero;

        public InventoryManaComponentObserver(Inventory inventory, Hero hero)
        {
            _inventory = inventory;
            _hero = hero;
            _inventory.OnConsumed += InventoryOnOnConsumed;
        }

        private void InventoryOnOnConsumed(InventoryItem inventoryItem)
        {
            if (inventoryItem.TryGetComponent<InventoryItem_ManaEffectComponent>(out var component))
            {
                _hero.Mana += component.ManaValue;
            }   
        }
    }
}