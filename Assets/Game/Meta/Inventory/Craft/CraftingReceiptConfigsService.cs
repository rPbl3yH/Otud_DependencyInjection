using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Game.Meta
{
    [CreateAssetMenu(
        fileName = "CraftingReceiptConfigsService",
        menuName = "Configs/Craft/New CraftingReceiptConfigsService"
    )]
    public class CraftingReceiptConfigsService : ScriptableObject
    {
        public List<InventoryItemReceipt> Receipts = new();

        public bool TryGetReceipt(string id, out InventoryItemReceipt itemReceipt)
        {
            itemReceipt = Receipts.FirstOrDefault(inventoryItemReceipt => inventoryItemReceipt.Id == id);

            if (itemReceipt == null)
            {
                return false;
            }

            return true;
        }
        
#if UNITY_EDITOR
        [Button]
        private void FindAllDetailConfigs()
        {
            Receipts.Clear();

            //Get all the materials that have the name gun in them using LoadMainAssetAtPath
            foreach (var asset in AssetDatabase.FindAssets("t:InventoryItemReceipt"))
            {
                var path = AssetDatabase.GUIDToAssetPath(asset);
                var itemReceipt = (InventoryItemReceipt)AssetDatabase.LoadMainAssetAtPath(path);

                Receipts.Add(itemReceipt);
            }
        }

        [Button]
        private void SetupAllIdWithNames()
        {
            foreach (var itemReceipt in Receipts)
            {
                itemReceipt.SetupIdWithName();
            }
        }
#endif
    }
}