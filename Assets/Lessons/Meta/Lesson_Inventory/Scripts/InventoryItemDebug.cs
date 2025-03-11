using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Meta.Lesson_Inventory
{
    public class InventoryItemDebug : MonoBehaviour
    {
        public InventoryItemConfig ItemConfig;

        [ReadOnly]
        public InventoryItem Item;

        public Inventory Inventory;
        public Hero Hero;

        private HeroItemsEffectsController _heroItemsEffectsController = new();
        private HeroItemsConsumeObserver _heroItemsConsumeObserver = new();

        private void Awake()
        {
            _heroItemsEffectsController.Construct(Inventory, Hero);
            _heroItemsConsumeObserver.Construct(Inventory, Hero);
        }

        private void OnDestroy()
        {
            _heroItemsEffectsController.OnDispose();
        }

        [Button]
        public void AddItem(InventoryItemConfig config)
        {
            var item = config.Prototype.Clone();
            InventoryUseCases.AddItem(Inventory, item);
        }

        [Button]
        public void RemoveItem(InventoryItemConfig config)
        {
            InventoryUseCases.RemoveItem(Inventory, config);
        }

        [Button]
        public void ConsumeItem(InventoryItemConfig config)
        {
            InventoryUseCases.ConsumeItem(Inventory, config);
        }
    }
}