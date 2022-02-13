namespace Game.Game
{
    public abstract class SystemUIAbstract : SystemAbstract
    {
        protected readonly EntitiesViewUI UIEs;

        protected SystemUIAbstract(in Entities ents, in EntitiesViewUI entsUI) : base(ents)
        {
            UIEs = entsUI;
        }
    }
}