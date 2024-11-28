using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities.Editor;

namespace Lessons.Meta.Lesson_Inventory
{
    [Serializable]
    public class Inventory
    {
        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemRemoved;
        public event Action<InventoryItem> OnItemConsumed; 
        
        public List<InventoryItem> Items = new();

        public void AddItem(InventoryItem prototype)
        {
            InventoryAddUseCases.AddItem(this, prototype);
            StackableInventoryAddUseCases.AddItem(this, prototype);
        }

        public bool RemoveItem(InventoryItem prototype)
        {
            return InventoryRemoveUseCases.RemoveItem(this, prototype);
        }

        public void Remove(InventoryItem item)
        {
            Items.Remove(item);
        }

        public InventoryItem FindItem(InventoryItem prototype)
        {
            var resultItem = Items.FirstOrDefault(item => item.Name == prototype.Name);
            return resultItem;
        }

        public void NotifyRemove(InventoryItem item)
        {
            OnItemRemoved?.Invoke(item);
        }

        public void ConsumeItem(InventoryItem item)
        {
            InventoryUseCases.ConsumeItem(this, item);
        }

        public void NotifyConsume(InventoryItem item)
        {
            OnItemConsumed?.Invoke(item);
        }

        public void Add(InventoryItem item)
        {
            Items.Add(item);
        }

        public InventoryItem FindLastItem(InventoryItem prototype)
        {
            var resultItem = Items.LastOrDefault(item => item.Name == prototype.Name);
            return resultItem;
        }
    }

    public static class InventoryAddUseCases
    {
        public static void AddItem(Inventory inventory, InventoryItem prototype)
        {
            inventory.Add(prototype);
        }
    }

    public static class StackableInventoryAddUseCases
    {
        public static void AddItem(Inventory inventory, InventoryItem prototype)
        {
            if (!CanStackable(prototype))
            {
                inventory.Add(prototype);
                return;
            }
            
            var lastItem = FindLastItem(inventory, prototype);

            if (lastItem == null)
            {
                inventory.Add(prototype);
                prototype.GetComponent<StackComponent>().Count = 1;
                return;
            }

            if (lastItem.TryGetComponent<StackComponent>(out var stackComponent))
            {
                if (stackComponent.Count == stackComponent.MaxCount)
                {
                    inventory.Add(prototype);
                    prototype.GetComponent<StackComponent>().Count = 1;
                    return;
                }

                stackComponent.Count++;
            }
        }
        
        private static bool CanStackable(InventoryItem inventoryItem)
        {
            return (inventoryItem.Flags & InventoryItemFlags.Stackable) == InventoryItemFlags.Stackable;
        }

        private static InventoryItem FindLastItem(Inventory inventory, InventoryItem prototype)
        {
            return inventory.FindLastItem(prototype);
        }
    }

    public static class InventoryUseCases
    {
        public static void ConsumeItem(Inventory inventory, InventoryItem item)
        {
            if (CanConsume(item) && inventory.RemoveItem(item))
            {
                inventory.NotifyConsume(item);
            }
        }

        private static bool CanConsume(InventoryItem item)
        {
            //0010 = Consumable
            //0100 & 0010 == 0010
            //0000 == 0010
            return (item.Flags & InventoryItemFlags.Consumable) == InventoryItemFlags.Consumable;
        }

        public static void AddItem(Inventory inventory, InventoryItem prototype)
        {
            if (!CanStackable(prototype))
            {
                inventory.Add(prototype);
                return;
            }
            
            var lastItem = FindLastItem(inventory, prototype);

            if (lastItem == null)
            {
                inventory.Add(prototype);
                prototype.GetComponent<StackComponent>().Count = 1;
                return;
            }

            if (lastItem.TryGetComponent<StackComponent>(out var stackComponent))
            {
                if (stackComponent.Count == stackComponent.MaxCount)
                {
                    inventory.Add(prototype);
                    prototype.GetComponent<StackComponent>().Count = 1;
                    return;
                }

                stackComponent.Count++;
            }
        }

        private static bool CanStackable(InventoryItem inventoryItem)
        {
            return (inventoryItem.Flags & InventoryItemFlags.Stackable) == InventoryItemFlags.Stackable;
        }

        private static InventoryItem FindLastItem(Inventory inventory, InventoryItem prototype)
        {
            return inventory.FindLastItem(prototype);
        }
    }

    public static class InventoryRemoveUseCases
    {
        public static bool RemoveItem(Inventory inventory, InventoryItem prototype)
        {
            var resultItem = inventory.FindItem(prototype);

            if (resultItem == null)
            {
                return false;
            }

            inventory.Remove(resultItem);
            inventory.NotifyRemove(resultItem);
            return true;
        }
    }
}