using UnityEngine;

namespace Game.Meta
{
    public sealed class ItemCraftUseCases
    {
        public static void StartCraft(Inventory inventory, InventoryItemReceipt receipt)
        {
            foreach (var ingredient in receipt.Ingredients)
            {
                inventory.RemovePrototypeItems(ingredient.ItemConfig.Item, ingredient.Amount);
            }
        }

        public static void CompleteCraft(Inventory inventory, InventoryItemReceipt receipt)
        {
            var item = receipt.ItemResult.Item;
            inventory.AddItems(item);
        }
        
        public static bool CanCraft(Inventory inventory, InventoryItemReceipt receipt,
            out CraftCheckInfo checkInfo, int count = 1)
        {
            var cloneReceipt = receipt.Clone(count);
            return CanCraft(inventory, cloneReceipt, out checkInfo);
        }
        
        public static bool CanCraft(Inventory inventory, InventoryItemReceipt receipt, out CraftCheckInfo checkInfo)
        {
            checkInfo = new CraftCheckInfo();
            var canCraft = true;

            foreach (var ingredient in receipt.Ingredients)
            {
                CraftCheckItemInfo checkItemInfo;
                if (!canCraft)
                {
                    HasIngredient(inventory, ingredient, out checkItemInfo);
                }
                else
                {
                    canCraft = HasIngredient(inventory, ingredient, out checkItemInfo);
                }
                
                checkInfo.ItemsInfo.Add(checkItemInfo);
            }

            return canCraft;
        }

        private static bool HasIngredient(Inventory inventory, InventoryItemIngredient ingredient,
            out CraftCheckItemInfo checkItemInfo)
        {
            checkItemInfo = new CraftCheckItemInfo();
            var currentAmount = inventory.GetItemCount(ingredient.ItemConfig.Item);
            
            checkItemInfo.CurrentCount = currentAmount;
            checkItemInfo.RequiredCount = ingredient.Amount;
            checkItemInfo.ItemConfig = ingredient.ItemConfig;
            
            if (currentAmount < ingredient.Amount)
            {
                return false;
            }

            return true;
        }

        public static bool CanCraft(Inventory inventory, InventoryItemReceipt receipt)
        {
            foreach (var ingredient in receipt.Ingredients)
            {
                var currentAmount = inventory.GetItemCount(ingredient.ItemConfig.Item);
                if (currentAmount < ingredient.Amount)
                {
                    return false;
                }
            }

            return true;
        }

        public static bool Craft(Inventory inventory, InventoryItemReceipt receipt)
        {
            if (!CanCraft(inventory, receipt))
            {
                Debug.Log($"Not enough resources to craft {receipt.ItemResult.Item.Id}");
                return false;
            }

            foreach (var ingredient in receipt.Ingredients)
            {
                inventory.RemovePrototypeItems(ingredient.ItemConfig.Item, ingredient.Amount);
            }

            var resultItem = receipt.ItemResult.Item.Clone();
            inventory.AddItems(resultItem);
            return true;
        }

        public static void StopCraft(Inventory inventory, InventoryItemReceipt itemReceipt)
        {
            foreach (var ingredient in itemReceipt.Ingredients)
            {
                inventory.AddItems(ingredient.ItemConfig.Item, ingredient.Amount);
            }
        }
    }
}