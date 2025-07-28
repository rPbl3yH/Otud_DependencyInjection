using System;

namespace Game.Meta
{
    [Serializable]
    public struct InventoryItemIngredient
    {
        public InventoryItemConfig ItemConfig;
        public int Amount;

        public void SetCount(int cloneIngredientAmount)
        {
            Amount = cloneIngredientAmount;
        }
    }
}