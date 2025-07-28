


namespace Game.Meta
{
    public class ExpComponent : IItemComponent
    {
        public int Exp;


        public IItemComponent Clone()
        {
            return new ExpComponent()
            {
                Exp = Exp,
            };
        }
    }
}
