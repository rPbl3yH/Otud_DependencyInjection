


namespace Game.Meta
{
    public class HardCurrencyComponent : IItemComponent
    {
        public IItemComponent Clone()
        {
            return new ConversionComponent();
        }
    }
}
