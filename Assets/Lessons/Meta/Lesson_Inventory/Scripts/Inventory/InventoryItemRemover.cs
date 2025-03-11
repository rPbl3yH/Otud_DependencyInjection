using System.Linq;

namespace Lessons.Meta.Lesson_Inventory
{
    public class InventoryItemRemover
    {
        private Inventory _inventory;

        public InventoryItemRemover(Inventory inventory)
        {
            _inventory = inventory;
        }
        
        public void RemoveItem(InventoryItem item)
        {
            var inventoryItem = _inventory.Items.FirstOrDefault(inventoryItem => inventoryItem.Id == item.Id);

            if (inventoryItem == null)
            {
                return;
            }

            _inventory.Items.Remove(inventoryItem);
        }

        public void RemoveItems(InventoryItem item, int count)
        {
            for (int i = 0; i < count; i++)
            {
                RemoveItem(item);
            }
        }
    }
}