namespace Game.Meta
{
    public class StackableItemRemover
    {
        private Inventory _inventory;

        public StackableItemRemover(Inventory inventory)
        {
            _inventory = inventory;
        }

        ///  <summary>
        /// <para>Removes one item</para>
        ///  </summary>
        ///  <param name="item">Target item in inventory.</param>
        ///  <param name="count"></param>
        public bool RemoveExistItem(InventoryItem item)
        {
            if (item == null)
            {
                return false;
            }

            if (!_inventory.HasItem(item))
            {
                return false;
            }

            return RemoveItem(item);
        }

        public bool RemovePrototypeItem(InventoryItem item)
        {
            if (item == null)
            {
                return false;
            }

            if (!_inventory.TryGetLastItem(item, out var lastItem))
            {
                return false;
            }

            return RemoveItem(lastItem);
        }

        private bool RemoveItem(InventoryItem item)
        {
            if (item.FlagsExists(InventoryItemFlags.Stackable))
            {
                return RemoveAsStackable(item);
            }

            return RemoveAsInstance(item);
        }

        private bool RemoveAsInstance(InventoryItem item)
        {
            Remove(item);
            return true;
        }

        private bool RemoveAsStackable(InventoryItem item)
        {
            var stackableComponent = item.GetComponent<StackableComponent>();
            var oldCount = stackableComponent.Count;
            stackableComponent.Count--;
            
            if (stackableComponent.Count > 0)
            {
                _inventory.NotifyCountChange(item, oldCount, stackableComponent.Count);
                return true;
            }

            Remove(item);

            return true;
        }
        
        public bool Remove(InventoryItem item)
        {
            _inventory.Items.Remove(item);
            _inventory.NotifyRemove(item);
            return true;
        }

        public bool CanRemove(InventoryItem item)
        {
            return _inventory.HasItem(item);
        }
    }
}