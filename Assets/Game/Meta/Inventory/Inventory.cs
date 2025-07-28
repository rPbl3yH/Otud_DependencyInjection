using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;

namespace Game.Meta
{
    [Serializable]
    public class Inventory
    {
        public delegate void ItemCountChangeAction(InventoryItem item, int oldCount, int newCount); 
        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemRemoved;
        public event Action<InventoryItem> OnItemConsumed;
        public event ItemCountChangeAction OnItemCountChanged;

        public int MaxCount = -1;

        [ShowInInspector, ReadOnly]
        public int Count => Items.Count;
        public event Action<int> OnCountChanged;

        [ShowInInspector, ReadOnly]
        public List<InventoryItem> Items = new();

        private StackableItemAdder _itemAdder;
        private StackableItemRemover _itemRemover;
        private StackableItemCounter _itemCounter;

        private InventoryItemConsumer _itemConsumer;
        
        public bool HasItems => Items.Count > 0;

        public Inventory()
        {
            _itemAdder = new StackableItemAdder(this);
            _itemRemover = new StackableItemRemover(this);
            _itemCounter = new StackableItemCounter(this);
            _itemConsumer = new InventoryItemConsumer(this);
        }
        
        public Inventory(bool isMaxCountInfinity)
        {
            _itemAdder = new StackableItemAdder(this, isMaxCountInfinity);
            _itemRemover = new StackableItemRemover(this);
            _itemCounter = new StackableItemCounter(this);
            _itemConsumer = new InventoryItemConsumer(this);
        }

        public void AddItems(InventoryItem item, int count = 1)
        {
            _itemAdder.AddItemsByPrototype(item, count);
            OnCountChanged?.Invoke(Count);
        }
        
        public void AddItems(List<InventoryItem> items)
        {
            _itemAdder.AddItems(items);
        }

        public bool CanAddItem(InventoryItem item)
        {
            return _itemAdder.CanAddItem(item);
        }

        public int GetItemCount(InventoryItem item)
        {
            return _itemCounter.GetItemCount(item);
        }

        public bool TryGetLastItem(InventoryItem item, out InventoryItem resultItem)
        {
            bool Predicate(InventoryItem inventoryItem) => InventoryUseCases.IsEquals(item, inventoryItem);
            return this.FindLastItem(Predicate, out resultItem);
        }

        public bool RemoveExistItems(InventoryItem item, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                RemoveExistItem(item);
            }

            return true;
        }

        public bool RemoveExistItem(InventoryItem item)
        {
            bool remove = _itemRemover.RemoveExistItem(item);
            OnCountChanged?.Invoke(Count);
            return remove;
        }

        public bool DeleteItem(InventoryItem item)
        {
            bool remove = _itemRemover.Remove(item);
            OnCountChanged?.Invoke(Count);
            return remove;
        }
        
        public bool RemovePrototypeItems(InventoryItem item, int count = 1)
        {
            for (int i = 0; i < count; i++)
            {
                RemovePrototypeItem(item);
            }

            return true;
        }

        public bool RemovePrototypeItem(InventoryItem item)
        {
            bool remove = _itemRemover.RemovePrototypeItem(item);
            OnCountChanged?.Invoke(Count);
            return remove;
        }

        public bool IsEqualItem(InventoryItem item, InventoryItem otherItem)
        {
            return InventoryUseCases.IsEquals(item, otherItem);
        }

        public bool CanRemove(InventoryItem item)
        {
            return _itemRemover.CanRemove(item);
        }

        public bool ConsumeItem(InventoryItem item)
        {
            return _itemConsumer.Consume(item);
        }

        public void AddItems(InventoryItemsList itemsList)
        {
            _itemAdder.AddItems(itemsList);
            OnCountChanged?.Invoke(Count);
        }

        public void AddItems(InventoryItemConfig[] itemConfigs)
        {
            _itemAdder.AddItems(itemConfigs);
        }

        public void AddItems(InventoryItemConfig itemConfig, int count = 1)
        {
            _itemAdder.AddItems(itemConfig, count);
        }

        public List<InventoryItem> GetItems(InventoryItemType itemType)
        {
            return Items.Where(item => item.Type == itemType).ToList();
        }

        // public bool TrySetCountIdenticalItem(InventoryItem inventoryItem, int count)
        // {
        //     if (Items.TryGetValue(inventoryItem, out var count))
        //     {
        //         
        //     }
        // }

        public void NotifyAdd(InventoryItem item)
        {
            OnItemAdded?.Invoke(item);
        }

        public void NotifyRemove(InventoryItem item)
        {
            OnItemRemoved?.Invoke(item);
        }
        
        public void NotifyCountChange(InventoryItem item, int oldCount, int newCount)
        {
            OnItemCountChanged?.Invoke(item, oldCount, newCount);
        }

        public void NotifyConsume(InventoryItem item)
        {
            OnItemConsumed?.Invoke(item);
        }

        [Button]
        public void Clear()
        {
            for (var index = Items.Count - 1; index >= 0; index--)
            {
                DeleteItem(Items[index]);
            }
        }

        public void SetSize(int result)
        {
            MaxCount = result;
        }
    }
}