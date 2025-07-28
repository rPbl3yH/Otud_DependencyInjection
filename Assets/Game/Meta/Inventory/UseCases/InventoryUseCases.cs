using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Meta
{
    public class InventoryUseCases
    {
        public static bool CopyItemTo(InventoryItem item, Inventory inventory)
        {
            var itemInventory = item.Owner;

            if (itemInventory == inventory)
            {
                return false;
            }

            if (!itemInventory.CanRemove(item) || !inventory.CanAddItem(item))
            {
                return false;
            }
            
            inventory.AddItems(item);
            // Debug.Log($"Transfer Item = {item.Id}");
            return true;
        }

        public static bool TransferAllItems(Inventory ownerInventory, Inventory targetInventory)
        {
            for (var index = ownerInventory.Items.Count - 1; index >= 0; index--)
            {
                InventoryItem item = ownerInventory.Items[index];
                TransferAllItems(item, targetInventory);
            }

            return true;
        }

        public static bool TransferAllItems(InventoryItem item, Inventory targetInventory)
        {
            var itemsCount = item.Owner.GetItemCount(item);

            if (itemsCount == 0)
            {
                return false;
            }

            for (int i = 0; i < itemsCount; i++)
            {
                TransferItem(item, targetInventory);
            }

            return true;
        }
                
        public static bool TransferItem(InventoryItem item, Inventory targetInventory)
        {
            var ownerInventory = item.Owner;

            if (ownerInventory == null)
            {
                Debug.LogError("Owner inventory is null");
                return false;
            }

            if (targetInventory == null)
            {
                Debug.LogError("Target inventory is null");
                return false;
            }

            if (ownerInventory == targetInventory)
            {
                return false;
            }

            var prototype = item.Clone();
            
            if (!ownerInventory.CanRemove(item))
            {
                return false;
            }

            if (!targetInventory.CanAddItem(item))
            {
                return false;
            }

            if (prototype.TryGetComponent(out StackableComponent stackableComponent))
            {
                stackableComponent.Count = 1;
            }

            ownerInventory.RemoveExistItem(item);
            targetInventory.AddItems(prototype);
            // Debug.Log($"Transfer Item = {item.Id}");
            return true;
        }

        public static bool IsEquals(InventoryItem item, InventoryItem otherItem)
        {
            return IsEquals(item, otherItem.Id);
        }
        
        public static bool IsEquals(InventoryItem item, string itemId)
        {
            return item.Id == itemId;
        }

        public static int GetItemCount(InventoryItem item)
        {
            var count = 1;

            if (item.FlagsExists(InventoryItemFlags.Stackable) 
                && item.TryGetComponent(out StackableComponent stackableComponent))
            {
                count = stackableComponent.Count;
            }

            return count;
        }

        public static bool TryGetItemCount(InventoryItem item, out int count)
        {
            count = 0;

            if (!item.FlagsExists(InventoryItemFlags.Stackable) 
                || !item.TryGetComponent(out StackableComponent stackableComponent))
            {
                return false;
            }

            count = stackableComponent.Count;
            return true;
        }

        public static void ConverseAllItemsInInventory(Inventory inventory)
        {
            for (int i = 0; i < inventory.Items.Count; i++)
            {
                if (inventory.Items[i].TryGetComponent(out ConversionComponent conversionComponent) == false)
                {
                    continue;
                }

                ConverseItem(inventory, inventory.Items[i]);
            }
        }


        public static void ConverseItem(Inventory inventory, InventoryItem item)
        {
            if (item.TryGetComponent(out ConversionComponent conversionComponent) == false)
            {
                Debug.LogError("Item don't have ConversionComponent: " + item.Id);
                return;
            }

            int itemsCount = 1;

            if (item.TryGetComponent(out StackableComponent stackableComponent) == true)
            {
                itemsCount = stackableComponent.Count;
            }

            for (int i = 0; i < conversionComponent.Ingredients.Length; i++)
            {
                inventory.AddItems(conversionComponent.Ingredients[i].ItemConfig.InstantiateItem(), conversionComponent.Ingredients[i].Amount * itemsCount);
            }

            inventory.DeleteItem(item);
        }
        
        public static List<InventoryItem> GetSortedItems(List<InventoryItem> items, ItemsSortType itemsSortType)
        {
            switch (itemsSortType)
            {
                case ItemsSortType.Rarity:
                    return items.OrderByDescending(item => item.Rarity).ToList();
                case ItemsSortType.Quantity:
                    return items.OrderByDescending(GetItemCount).ToList();
                default:
                    throw new ArgumentOutOfRangeException(nameof(itemsSortType), itemsSortType, null);
            }
        }
    }
    
    public enum ItemsSortType
    {
        Rarity,
        Quantity
    }
}