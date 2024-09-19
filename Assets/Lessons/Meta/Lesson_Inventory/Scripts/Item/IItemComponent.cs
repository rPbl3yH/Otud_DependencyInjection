namespace Lessons.Meta.Lesson_Inventory
{
    public interface IItemComponent
    {
        IItemComponent Clone();
    }

    public interface IEffectibleComponent
    {
        void Apply();
        void Discard();
    }
}