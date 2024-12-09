using System;
using System.Collections.Generic;
using Lessons.Meta.Lesson_Inventory;
using UnityEngine;

namespace Lessons.Meta.Lesson_TDD
{
    [CreateAssetMenu(
        fileName = "ItemReceipt",
        menuName = "Lessons/Craft/New ItemReceipt"
    )]
    [Serializable]
    public class ItemReceipt : ScriptableObject
    {
        public InventoryItemConfig ResultItemConfig;
        public List<ItemIngredient> Ingredients = new();
    }
}