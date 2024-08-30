using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Meta.Lesson_Inventory
{
    public class InventoryDebug : MonoBehaviour
    {
        public Hero Hero;
        public Inventory Inventory = new Inventory();
        public InventoryItemConfig ItemConfig;
        public InventoryEffectObserver EffectObserver;

        private void Awake()
        {
            // EffectObserver = new InventoryEffectObserver(Inventory);
            // EffectObserver.Construct();

            var healthComponentObserver = new InventoryHealthComponentObserver(Inventory, Hero);
            var manaComponentObserver = new InventoryManaComponentObserver(Inventory, Hero);
        }

        [Button]
        public void AddItem(InventoryItemConfig itemConfig)
        {
            var item = itemConfig.Item.Clone();
            Inventory.AddItem(item);
        }

        [Button]
        public void RemoveItem(InventoryItemConfig itemConfig)
        {
            var item = itemConfig.Item.Clone();
            Inventory.RemoveItem(item);
        }

        [Button]
        public void ConsumeItem(InventoryItemConfig itemConfig)
        {
            var item = itemConfig.Item.Clone();
            Inventory.ConsumeItem(item);
        }
    }
}