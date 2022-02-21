namespace Game.Game
{
    public abstract class SystemUIAbstract : SystemAbstract
    {
        protected readonly EntitiesViewUI UIEs;

        protected SystemUIAbstract(in EntitiesModel ents, in EntitiesViewUI entsUI) : base(ents)
        {
            UIEs = entsUI;
        }
    }
}