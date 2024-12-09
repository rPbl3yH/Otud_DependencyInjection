using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Lessons.Meta.Lesson_Inventory
{
    //Installer
    public class InventoryDebug : MonoBehaviour
    {
        public Hero Hero;
        public Inventory Inventory;

        //Inject
        private readonly List<IInventoryObserver> _inventoryObservers = new();
        private ManaPoisonConsumer _manaPoisonConsumer;

        private void Awake()
        {
            _manaPoisonConsumer = new ManaPoisonConsumer(Inventory, Hero);
        }

        private void Start()
        {
            _inventoryObservers.Add(new SwordInventoryObserver(Hero));
        }

        //Start game
        private void OnEnable()
        {
            Inventory.OnItemAdded += OnItemAdded;
            Inventory.OnItemRemoved += OnItemRemoved;
            _manaPoisonConsumer.OnStartGame();
        }

        //Finish game
        private void OnDisable()
        {
            Inventory.OnItemAdded -= OnItemAdded;
            Inventory.OnItemRemoved -= OnItemRemoved;
            _manaPoisonConsumer.OnFinishGame();
        }

        private void OnItemRemoved(InventoryItem inventoryItem)
        {
            foreach (var inventoryObserver in _inventoryObservers)
            {
                inventoryObserver.OnItemRemoved(inventoryItem);
            }
        }

        private void OnItemAdded(InventoryItem inventoryItem)
        {
            foreach (var inventoryObserver in _inventoryObservers)
            {
                inventoryObserver.OnItemAdded(inventoryItem);
            }
        }

        //Debug
        [Button]
        public void AddItem(InventoryItemConfig inventoryItemConfig)
        {
            var item = inventoryItemConfig.Prototype.Clone();
            Inventory.AddItem(item);
        }

        [Button]
        public void RemoveItem(InventoryItemConfig inventoryItemConfig)
        {
            var item = inventoryItemConfig.Prototype.Clone();
            Inventory.RemoveItem(item);
        }

        [Button]
        public void ConsumeItem(InventoryItemConfig inventoryItemConfig)
        {
            var item = inventoryItemConfig.Prototype.Clone();
            Inventory.ConsumeItem(item);
        }
    }
}