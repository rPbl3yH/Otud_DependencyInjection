namespace Game.Meta
{
    public class CraftCheckItemInfo
    {
        public InventoryItemConfig ItemConfig;
        public int CurrentCount;
        public int RequiredCount;

        public bool IsEnough => CurrentCount >= RequiredCount;
    }
}