


namespace Game.Meta
{
    public class ConversionComponent : IItemComponent
    {
        public InventoryItemIngredient[] Ingredients;


        public IItemComponent Clone()
        {
            return new ConversionComponent()
            {
                Ingredients = Ingredients,
            };
        }
    }
}
