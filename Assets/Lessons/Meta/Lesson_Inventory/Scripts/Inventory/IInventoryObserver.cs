using System;

namespace Lessons.Meta.Lesson_Inventory
{
    public interface IInventoryObserver
    {
        void OnItemAdded(InventoryItem item);
        void OnItemRemoved(InventoryItem item);
    }

    public class ManaPoisonConsumer
    {
        private readonly Inventory _inventory;
        private readonly Hero _hero;

        public ManaPoisonConsumer(Inventory inventory, Hero hero)
        {
            _inventory = inventory;
            _hero = hero;
        }

        public void OnStartGame()
        {
            _inventory.OnItemConsumed += OnItemConsumed;
        }

        public void OnFinishGame()
        {
            _inventory.OnItemConsumed -= OnItemConsumed;
        }

        private void OnItemConsumed(InventoryItem inventoryItem)
        {
            if (inventoryItem.TryGetComponent<ManaComponent>(out var manaComponent))
            {
                _hero.Mana += manaComponent.Mana;
            }
        }
    }

    [Serializable]
    public class ManaComponent : IItemComponent
    {
        public int Mana = 3;
        
        public IItemComponent Clone()
        {
            return new ManaComponent()
            {
                Mana = Mana,
            };
        }
    }
}