using System;
using System.Collections.Generic;
using UnityEngine;

namespace Lessons.Meta.Lesson_Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public string Name;
        
        //Meta Data
        public string Description;
        public Sprite Icon;

        public ItemFlags Flags;

        [SerializeReference]
        public List<IItemComponent> ItemComponents = new List<IItemComponent>();

        public IReadOnlyList<IItemComponent> GetComponents()
        {
            return ItemComponents;
        }

        public bool TryGetComponent<T>(out T component)
        {
            foreach (IItemComponent itemComponent in ItemComponents)
            {
                if (itemComponent is T tComponent)
                {
                    component = tComponent;
                    return true;
                }
            }

            component = default;
            return false;
        }

        public InventoryItem Clone()
        {
            return new InventoryItem()
            {
                Name = Name,
                Description = Description,
                Icon = Icon,
                Flags = Flags,
                ItemComponents = CloneComponents(),
            };
        }

        private List<IItemComponent> CloneComponents()
        {
            var list = new List<IItemComponent>();

            foreach (IItemComponent itemComponent in ItemComponents)
            {
                list.Add(itemComponent.Clone());
            }

            return list;
        }
    }
}
