using System;
using UnityEngine;

namespace Lessons.Meta.Lesson_Inventory
{
    [Serializable]
    public class InventoryItemMetaData
    {
        public string Name = string.Empty;
        public string Description = string.Empty;
        public Sprite Icon;
    }
}