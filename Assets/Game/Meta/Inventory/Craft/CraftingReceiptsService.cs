using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Meta
{
    [Serializable]
    public class CraftingReceiptsService
    {
        [ReadOnly] public List<InventoryItemReceipt> Receipts = new();

        [SerializeField] private CraftingReceiptsLevelConfigs _receiptsLevelConfigs;

        public void AddLevelReceipts(int level)
        {
            var index = level - 1;
            var receipts = _receiptsLevelConfigs.Receipts[index].List;
            AddReceipts(receipts);
        }

        private void AddReceipts(List<InventoryItemReceipt> receipts)
        {
            Receipts.AddRange(receipts);
        }
    }
}