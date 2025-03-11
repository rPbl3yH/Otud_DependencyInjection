namespace Lessons.Meta.Lesson_Inventory
{
    public class InventoryItemAdder
    {
        private Inventory _inventory;

        public InventoryItemAdder(Inventory inventory)
        {
            _inventory = inventory;
        }
        
        public void AddItem(InventoryItem item)
        {
            _inventory.Items.Add(item);
        }

        public void AddItem(InventoryItemConfig itemConfig)
        {
            var item = itemConfig.Prototype.Clone();
            AddItem(item);
        }
    }
}