namespace Game.Meta
{
    public class InventoryItemAdder
    {
        private Inventory _inventory;

        public InventoryItemAdder(Inventory inventory)
        {
            _inventory = inventory;
        }

        public bool IsEnoughCapacity()
        {
            return _inventory.Count < _inventory.MaxCount;
        }

        public bool CanAddItem()
        {
            return IsEnoughCapacity();
        }
        
        public void AddItem(InventoryItem item)
        {
            if (!CanAddItem())
            {
                return;
            }
            
            item.Owner = _inventory;
            _inventory.NotifyAdd(item);
        }

        public void AddItems(InventoryItemsList itemsList)
        {
            foreach (var loot in itemsList.Loots)
            {
                var item = loot.ItemConfig.Item.Clone();
                AddItem(item);
            }
        }
    }
}