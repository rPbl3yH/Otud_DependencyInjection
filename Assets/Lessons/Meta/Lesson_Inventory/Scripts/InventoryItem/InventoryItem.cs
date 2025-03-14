using System;
using UnityEngine;

namespace Lessons.Meta.Lesson_Inventory
{
    [Serializable]
    public class InventoryItem
    {
        public string Id;
        public InventoryItemMetaData MetaData = new();
        public ItemFlags Flags;
        
        [SerializeReference]
        public IItemComponent[] Components;

        public InventoryItem()
        {
            
        }
        
        public InventoryItem(string id)
        {
            Id = id;
        }

        public InventoryItem Clone()
        {
            var copiedComponents = Array.Empty<IItemComponent>();

            if (Components != null)
            {
                copiedComponents = new IItemComponent[Components.Length];

                for (var index = 0; index < Components.Length; index++)
                {
                    IItemComponent component = Components[index];
                    copiedComponents[index] = component.Clone();
                }
            }
            
            return new InventoryItem()
            {
                Id = Id,
                MetaData = new InventoryItemMetaData()
                {
                    Name = MetaData.Name,
                    Description = MetaData.Description,
                    Icon = MetaData.Icon
                },
                Flags = Flags,
                Components = copiedComponents
            };
        }

        public bool TryGetComponent<T>(out T component)
        {
            foreach (var itemComponent in Components)
            {
                if (itemComponent is T targetComponent)
                {
                    component = targetComponent;
                    return true;
                }
            }

            component = default;
            return false;
        }
    }

    public interface IItemComponent
    {
        IItemComponent Clone();
    }

    [Serializable]
    public class StackableItemComponent : IItemComponent
    {
        public int Count;
        public int MaxCount;
        
        public IItemComponent Clone()
        {
            return new StackableItemComponent()
            {
                Count = Count,
                MaxCount = MaxCount,
            };
        }
    }
}