using System.Collections.Generic;
using Lessons.Meta.Lesson_Inventory;
using UnityEngine;

namespace Lessons.Meta.Lesson_Crafting
{
    [CreateAssetMenu(
        fileName = "ItemReceipt",
        menuName = "Lessons/Configs/New ItemReceipt"
    )]
    public class ItemReceipt : ScriptableObject
    {
        public InventoryItemConfig ResultItem;
        public List<ReceiptIngredient> Ingredients = new();
    }
}