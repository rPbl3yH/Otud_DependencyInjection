namespace Game.Meta
{
    public interface IItemComponent
    {
        IItemComponent Clone();
    }

    public interface IDescriptionItemComponent
    {
        string GetDescription();
    }
}