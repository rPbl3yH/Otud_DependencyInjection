using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Meta
{
    [CreateAssetMenu(
        fileName = "CraftingReceiptsLevelConfigs",
        menuName = "Configs/Crafting/New CraftingReceiptsLevelConfigs"
    )]
    public class CraftingReceiptsLevelConfigs : ScriptableObject
    {
        [ListDrawerSettings(OnBeginListElementGUI = nameof(DrawLevels))] 
        public List<InventoryItemReceipts> Receipts = new();
        
        private void DrawLevels(int index)
        {
            GUILayout.Space(8);
            GUILayout.Label($"Level {index + 1}");
        }
    }

    [Serializable]
    public class InventoryItemReceipts
    {
        public List<InventoryItemReceipt> List = new();
    }
}