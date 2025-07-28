namespace Game.Meta
{
    public class InventoryItemConsumer
    {
        private Inventory _inventory;

        public InventoryItemConsumer(Inventory inventory)
        {
            _inventory = inventory;
        }

        public bool CanConsume(InventoryItem item)
        {
            if ((item.Flags & InventoryItemFlags.Consumable) != InventoryItemFlags.Consumable)
            {
                return false;
            }

            if (!_inventory.CanRemove(item))
            {
                return false;
            }
            
            return true;
        }

        public bool Consume(InventoryItem item)
        {
            if (!CanConsume(item))
            {
                return false;
            }
            
            _inventory.RemoveExistItem(item);
            _inventory.NotifyConsume(item);
            return true;
        }
    }
}