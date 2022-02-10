namespace Game.Game
{
    public abstract class SystemUIAbstract : SystemAbstract
    {
        protected readonly EntitiesUI UIEs;

        protected SystemUIAbstract(in Entities ents, in EntitiesUI entsUI) : base(ents)
        {
            UIEs = entsUI;
        }
    }
}