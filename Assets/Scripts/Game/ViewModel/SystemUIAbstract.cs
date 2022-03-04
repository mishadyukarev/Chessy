namespace Chessy.Game
{
    public abstract class SystemUIAbstract : SystemAbstract
    {
        protected readonly EntitiesViewUI UIE;

        protected SystemUIAbstract( in EntitiesViewUI entsUI, in EntitiesModel ents) : base(ents)
        {
            UIE = entsUI;
        }
    }
}