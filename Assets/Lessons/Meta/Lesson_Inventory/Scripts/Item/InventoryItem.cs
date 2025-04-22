using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Lessons.Meta.Lesson_Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public string Name;
        public ItemMetaData MetaData;

        public InventoryItemFlags Flags;

        [SerializeReference]
        public IItemComponent[] ItemComponents;

        public InventoryItem()
        {
        }

        public InventoryItem(string name, InventoryItemFlags flags = InventoryItemFlags.None,
            IItemComponent[] itemComponents = null)
        {
            Name = name;
            Flags = flags;
            ItemComponents = itemComponents;
        }

        public InventoryItem Clone()
        {
            var item = new InventoryItem()
            {
                Name = Name,
                Flags = Flags,
                MetaData = CloneMetadata(),
                ItemComponents = CloneComponents()
            };

            return item;
        }

        public T GetComponent<T>() where T : IItemComponent
        {
            foreach (var itemComponent in ItemComponents)
            {
                if (itemComponent is T component)
                {
                    return component;
                }
            }

            return default;
        }

        private ItemMetaData CloneMetadata()
        {
            return new ItemMetaData()
            {
                Description = MetaData.Description,
                Icon = MetaData.Icon,
            };
        }

        private IItemComponent[] CloneComponents()
        {
            var list = new List<IItemComponent>();
            
            foreach (IItemComponent itemComponent in ItemComponents)
            {
                list.Add(itemComponent.Clone());    
            }

            return list.ToArray();
        }

        public bool TryGetComponent<T>(out T resultComponent)
        {
            foreach (var itemComponent in ItemComponents)
            {
                if (itemComponent is T component)
                {
                    resultComponent = component;
                    return true;
                }
            }

            resultComponent = default;
            return false;
        }
    }
}