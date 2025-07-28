using System;

namespace Game.Meta
{
    public static class InventoryExtensions
    {
        public static bool FlagsExists(this InventoryItem it, InventoryItemFlags flags)
        {
            return (it.Flags & flags) == flags;
        }
        
        public static bool FlagsExists(this InventoryItemConfig it, InventoryItemFlags flags)
        {
            return (it.Item.Flags & flags) == flags;
        }
        
        public static bool FindFirstItem(this Inventory inventory, Func<InventoryItem, bool> predicate, 
            out InventoryItem inventoryItem)
        {
            foreach (var item in inventory.Items)
            {
                if (predicate.Invoke(item))
                {
                    inventoryItem = item;
                    return true;
                }
            }

            inventoryItem = default;
            return false;
        }
        
        public static bool FindLastItem(this Inventory inventory, Func<InventoryItem, bool> predicate, 
            out InventoryItem inventoryItem)
        {
            inventoryItem = null;
            
            foreach (var itemPair in inventory.Items)
            {
                if (predicate.Invoke(itemPair))
                {
                    inventoryItem = itemPair;
                }
            }

            if (inventoryItem == null)
            {
                return false;
            }

            return true;
        }
        
        public static bool TryFindItem(this Inventory inventory, InventoryItem item, out InventoryItem inventoryItem)
        {
            return TryFindItem(inventory, item.Id, out inventoryItem);
        }
        
        public static bool TryFindItem(this Inventory inventory, string itemId, out InventoryItem inventoryItem)
        {
            return FindFirstItem(inventory, otherItem => InventoryUseCases.IsEquals(otherItem, itemId), out inventoryItem);
        }

        public static bool HasItem(this Inventory inventory, InventoryItem item)
        {
            return TryFindItem(inventory, item, out _);
        }
        
        public static bool HasItems(this Inventory inventory, InventoryItem item, int count)
        {
            var itemCount = inventory.GetItemCount(item);
            return itemCount >= count;
        }
    }
}