using System.Collections.Generic;

namespace Game.Meta
{
    public class StackableItemAdder
    {
        private readonly Inventory _inventory;
        private readonly bool _isInfinityMaxCount;

        public StackableItemAdder(Inventory inventory, bool isInfinityMaxCount = false)
        {
            _inventory = inventory;
            _isInfinityMaxCount = isInfinityMaxCount;
        }

        public void AddItemsByPrototype(InventoryItem prototype, int count)
        {
            // Debug.Log($"Add item {prototype.Id}. Count = {count}");
            if (prototype.FlagsExists(InventoryItemFlags.Stackable))
            {
                var stackableComponent = prototype.GetComponent<StackableComponent>();
                count *= stackableComponent.Count;
                AddAsStackable(prototype, count);
            }
            else
            {
                AddAsCommon(prototype, count);
            }
        }

        private void AddAsCommon(InventoryItem prototype, int count)
        {
            for (var i = 0; i < count; i++)
            {
                AddCommonItem(prototype);
            }
        }

        private void AddCommonItem(InventoryItem prototype)
        {
            if (!CanAddItem(prototype))
            {
                 return;   
            }
            
            var item = prototype.Clone();
            AddItem(item);
        }

        private void AddAsStackable(InventoryItem prototype, int count)
        {
            while (count > 0)
            {
                if (_inventory.FindFirstItem(IsAvailable, out var targetItem))
                {
                    var stackableComponent = targetItem.GetComponent<StackableComponent>();
                    var stackSize = stackableComponent.MaxCount;
                    var oldCount = stackableComponent.Count;
                    var newCount = IncrementValueInStack(targetItem, stackSize, ref count);
                    _inventory.NotifyCountChange(targetItem, oldCount, newCount);
                    
                    if (!CanAddItem(targetItem))
                    {
                        break;
                    }
                }
                else
                {
                    if (!CanAddItem(prototype))
                    {
                        break;
                    }
                    
                    targetItem = prototype.Clone();
                    var stackableComponent = targetItem.GetComponent<StackableComponent>();
                    stackableComponent.Count = 0;
                    
                    TrySetMaxCount(stackableComponent);
                    
                    var stackSize = stackableComponent.MaxCount;
                    var newCount = IncrementValueInStack(targetItem, stackSize, ref count);
                    stackableComponent.Count = newCount;
                    AddItem(targetItem);
                }
            }

            bool IsAvailable(InventoryItem otherItem)
            {
                return InventoryUseCases.IsEquals(otherItem, prototype) 
                       && !otherItem.GetComponent<StackableComponent>().IsFull;
            }
        }

        private void TrySetMaxCount(StackableComponent stackableComponent)
        {
            if (_isInfinityMaxCount)
            {
                stackableComponent.MaxCount = int.MaxValue;
            }
        }

        private int IncrementValueInStack(InventoryItem item, int stackSize, ref int remainingCount)
        {
            var stackableComponent = item.GetComponent<StackableComponent>();
            var previousCount = stackableComponent.Count;
            var newCount = previousCount + remainingCount;

            var overflow = newCount - stackSize;
            if (overflow > 0)
            {
                newCount = stackSize;
            }

            stackableComponent.Count = newCount;

            var diff = newCount - previousCount;
            remainingCount -= diff;

            return newCount;
        }

        public void AddItems(InventoryItemsList itemsList)
        {
            foreach (var loot in itemsList.Loots)
            {
                var item = loot.ItemConfig.Item.Clone();

                if (!CanAddItem(item))
                {
                    return;
                }

                var count = loot.Count;
                
                AddItemsByPrototype(item, count);
            }
        }

        private bool IsEnoughCapacity()
        {
            return _inventory.Count < _inventory.MaxCount;
        }

        public bool CanAddItem(InventoryItem item)
        {
            if (_inventory.MaxCount == -1)
            {
                return true;
            }

            if (!item.TryGetComponent(out StackableComponent _))
            {
                return IsEnoughCapacity();
            }
            
            foreach (var existingItem in _inventory.Items)
            {
                if (existingItem.Id == item.Id &&
                    existingItem.TryGetComponent(out StackableComponent existingStack))
                {
                    if (existingStack.Count < existingStack.MaxCount)
                    {
                        return true; // Есть место в существующем стеке
                    }
                }
            }

            return IsEnoughCapacity();
        }

        private void AddItem(InventoryItem item)
        {
            _inventory.Items.Add(item);
            item.Owner = _inventory;
            _inventory.NotifyAdd(item);
        }

        public void AddItems(InventoryItemConfig itemConfig, int count = 1)
        {
            AddItemsByPrototype(itemConfig.Item.Clone(), count);
        }

        public void AddItems(InventoryItemConfig[] itemConfigs)
        {
            foreach (var itemConfig in itemConfigs)
            {
                AddItems(itemConfig);
            }
        }

        public void AddItems(List<InventoryItem> items)
        {
            foreach (InventoryItem item in items)
            {
                AddItemsByPrototype(item, 1);
            }
        }
    }
}