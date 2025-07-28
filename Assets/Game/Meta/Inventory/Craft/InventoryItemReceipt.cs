using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Meta
{
    [CreateAssetMenu(
        fileName = "InventoryItemReceipt",
        menuName = "Configs/Receipt/InventoryItemReceipt"
    )]
    public sealed class InventoryItemReceipt : ScriptableObject
    {
        public string Id;
        public InventoryItemConfig ItemResult;
        public InventoryItemIngredient[] Ingredients;

        public RecipeType ReceiptType;
        public float CraftTime;

        public InventoryItemReceipt Clone(int ingredientMultiplier)
        {
            var config = CreateInstance<InventoryItemReceipt>();
            config.ItemResult = CreateInstance<InventoryItemConfig>();
            config.ItemResult.Item = ItemResult.InstantiateItem();
            config.Ingredients = new InventoryItemIngredient[Ingredients.Length];
            config.ReceiptType = ReceiptType;
            config.CraftTime = CraftTime;

            for (var index = 0; index < Ingredients.Length; index++)
            {
                InventoryItemIngredient ingredient = Ingredients[index];
                ingredient.Amount *= ingredientMultiplier;
                config.Ingredients[index] = ingredient;
            }

            return config;
        }

        [Button]
        public void SetupIdWithName()
        {
            Id = name;
        }
    }
}