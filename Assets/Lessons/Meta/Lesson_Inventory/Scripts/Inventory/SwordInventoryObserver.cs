namespace Lessons.Meta.Lesson_Inventory
{
    public class SwordInventoryObserver : IInventoryObserver
    {
        private Hero _hero;

        public SwordInventoryObserver(Hero hero)
        {
            _hero = hero;
        }

        void IInventoryObserver.OnItemAdded(InventoryItem item)
        {
            if (item.TryGetComponent<SwordComponent>(out var swordComponent))
            {
                _hero.Damage += swordComponent.Damage;
            }
        }

        void IInventoryObserver.OnItemRemoved(InventoryItem item)
        {
            if (item.TryGetComponent<SwordComponent>(out var swordComponent))
            {
                _hero.Damage -= swordComponent.Damage;
            }
        }
    }
}