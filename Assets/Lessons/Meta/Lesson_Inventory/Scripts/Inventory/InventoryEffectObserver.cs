namespace Lessons.Meta.Lesson_Inventory
{
    public class InventoryEffectObserver
    {
        private readonly Inventory _inventory;

        public InventoryEffectObserver(Inventory inventory)
        {
            _inventory = inventory;
        }

        public void Construct()
        {
            _inventory.OnAdded += OnItemAdded;
            _inventory.OnRemoved += OnItemRemoved;
        }

        private void OnItemAdded(InventoryItem inventoryItem)
        {
            //xxxx * 0100 = x1xx
            if ((inventoryItem.Flags & ItemFlags.Effectible) == ItemFlags.Effectible)
            {
                var components = inventoryItem.GetComponents();
                
                foreach (var component in components)
                {
                    if (component is IEffectibleComponent effectibleComponent)
                    {
                        effectibleComponent.Apply();
                    }
                }
            }
        }

        private void OnItemRemoved(InventoryItem inventoryItem)
        {
            if ((inventoryItem.Flags & ItemFlags.Effectible) == ItemFlags.Effectible)
            {
                var components = inventoryItem.GetComponents();
                
                foreach (var component in components)
                {
                    if (component is IEffectibleComponent effectibleComponent)
                    {
                        effectibleComponent.Discard();
                    }
                }
            }
        }
    }
}