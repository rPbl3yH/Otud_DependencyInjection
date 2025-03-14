using System;
using System.Collections.Generic;
using System.Linq;

namespace Lessons.Meta.Lesson_Inventory
{
    //Facade
    [Serializable]
    public class Inventory
    {
        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemRemoved;

        public event Action<InventoryItem> OnItemConsumed; 
        
        public List<InventoryItem> Items = new();

        private InventoryItemAdder _inventoryItemAdder;
        private InventoryItemRemover _inventoryItemRemover;

        private List<IInventoryObserver> _inventoryObservers;
        
        public Inventory()
        {
            _inventoryItemAdder = new InventoryItemAdder(this);
            _inventoryItemRemover = new InventoryItemRemover(this);
        }

        public void NotifyAddItem(InventoryItem item) => OnItemAdded?.Invoke(item);

        public void NotifyRemoveItem(InventoryItem item) => OnItemRemoved?.Invoke(item);

        public void NotifyConsumeItem(InventoryItem item) => OnItemConsumed?.Invoke(item);
    }

    public class InventoryUseCases
    {
        public static void AddItem(Inventory inventory, InventoryItem item)
        {
            inventory.Items.Add(item);
            inventory.NotifyAddItem(item);
        }
        
        public static void AddItem(Inventory inventory, InventoryItemConfig itemConfig)
        {
            var item = itemConfig.Prototype.Clone();
            AddItem(inventory, item);
        }

        public static void RemoveItem(Inventory inventory, InventoryItem item)
        {
            var inventoryItem = inventory.Items.FirstOrDefault(inventoryItem => inventoryItem.Id == item.Id);
            
            if (inventoryItem == null)
            {
                return;
            }

            inventory.Items.Remove(inventoryItem);
            inventory.NotifyRemoveItem(inventoryItem);
        }

        public static void RemoveItem(Inventory inventory, InventoryItemConfig config)
        {
            var inventoryItem = inventory.Items.FirstOrDefault(inventoryItem => inventoryItem.Id == config.Prototype.Id);

            if (inventoryItem == null)
            {
                return;
            }

            inventory.Items.Remove(inventoryItem);
            inventory.NotifyRemoveItem(inventoryItem);
        }

        public static void ConsumeItem(Inventory inventory, InventoryItemConfig itemConfig)
        {
            var item = itemConfig.Prototype;
            
            if (CanConsume(item))
            {
                RemoveItem(inventory, itemConfig);
                inventory.NotifyConsumeItem(item);
            }
        }

        public static bool CanConsume(InventoryItem item)
        {
            return HasFlag(item, ItemFlags.Consumable);
        }
        
        public static bool HasFlag(InventoryItem item, ItemFlags itemFlags)
        {
            return (item.Flags & itemFlags) == itemFlags;
        }

        public static int Sum(int a, int b)
        {
            return a + b;
        }

        public static bool HasItem(Inventory inventory, InventoryItem item)
        {
            return inventory.Items.Any(inventoryItem => inventoryItem.Id == item.Id);
        }

        public static int GetItemCount(Inventory inventory, InventoryItem item)
        {
            return inventory.Items.Count(inventoryItem => inventoryItem.Id == item.Id);
        }
    }
}