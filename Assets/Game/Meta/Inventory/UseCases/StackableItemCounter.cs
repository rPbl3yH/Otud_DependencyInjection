namespace Game.Meta
{
    public class StackableItemCounter
    {
        private readonly Inventory _inventory;

        public StackableItemCounter(Inventory inventory)
        {
            _inventory = inventory;
        }

        public int GetItemCount(InventoryItem item)
        {
            var result = 0;
            var items = _inventory.Items;
            
            foreach (var pair in items)
            {
                if (_inventory.IsEqualItem(pair, item))
                {
                    if (pair.FlagsExists(InventoryItemFlags.Stackable))
                    {
                        var stackableComponent = pair.GetComponent<StackableComponent>();
                        result += stackableComponent.Count;
                    }
                    else
                    {
                        result++;
                    }
                }
            }

            return result;
        }
    }
}