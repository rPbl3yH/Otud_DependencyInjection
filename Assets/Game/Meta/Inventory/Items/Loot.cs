using Sirenix.OdinInspector;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
#endif
using System;
using UnityEngine;


namespace Game.Meta
{
    [Serializable]
    public struct Loot
    {
        public Loot(InventoryItemConfig itemConfig, int count)
        {
            ItemConfig = itemConfig;
            Count = count;
        }


#if UNITY_EDITOR
        [GUIColor("RarityColor")]
#endif
        public InventoryItemConfig ItemConfig;

#if UNITY_EDITOR
        [GUIColor("RarityColor")]
#endif
        public int Count;
        
    }
}
