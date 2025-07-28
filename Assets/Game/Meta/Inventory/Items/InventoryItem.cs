using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Meta
{
    [Serializable]
    public class InventoryItem
    {
        [field: SerializeField]
        public string Id { get; set; }

        public MetaDataInventoryItem MetaData;

        [field: SerializeField]
        public InventoryItemType Type { get; set; }
        
        [field: SerializeField]
        public InventoryItemFlags Flags { get; set; }

#if UNITY_EDITOR
        [GUIColor("RarityColor")]
#endif
        [SerializeField]
        public ItemRarity Rarity;
        
        [SerializeReference]
        private IItemComponent[] _components;

        [NonSerialized]
        public Inventory Owner;
        
        public bool TryGetComponent<T>(out T component)
        {
            for (int i = 0; i < _components.Length; i++)
            {
                if (_components[i] is T itemComponent)
                {
                    component = itemComponent;
                    return true;
                }
            }

            component = default;
            return false;
        }

        public void InitComponents()
        {
            _components = Array.Empty<IItemComponent>();
        }
        
        public T GetComponent<T>()
        {
            for (int i = 0; i < _components.Length; i++)
            {
                if (_components[i] is T itemComponent)
                {
                    return itemComponent;
                }
            }

            return default;
        }

        public InventoryItem Clone()
        {
            return new InventoryItem()
            {
                Id = Id,
                MetaData = MetaData.Clone(),
                Type = Type,
                Flags = Flags,
                Rarity = Rarity,
                _components = CloneComponents()
            };
        }

        private IItemComponent[] CloneComponents()
        {
            if (_components == null)
            {
                _components = Array.Empty<IItemComponent>();
                return _components;
            }
            
            var length = _components.Length;
            var result = new IItemComponent[length];

            for (int i = 0; i < length; i++)
            {
                result[i] = _components[i].Clone();
            }

            return result;
        }

        public void AddComponent(IItemComponent itemComponent)
        {
            var list = _components == null? new List<IItemComponent>() : _components.ToList();
            list.Add(itemComponent);
            _components = list.ToArray();
        }
        
        public IItemComponent[] GetComponents()
        {
            return _components;
        }
    }

    [Flags]
    public enum InventoryItemFlags
    {
        None = 0,
        Consumable = 1,
        Stackable = 2,
        Equippable = 4,
        Effectible = 8,
        HardCurrency = 16,
        Deletable = 32
    }

    public enum InventoryItemType
    {
        None = 0,
        Resources = 1,
        Consumables = 2,
        BluePrints = 3,
        Others = 4,
    }
}