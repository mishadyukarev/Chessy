namespace Chessy.Game
{
    public abstract class SystemUIAbstract : SystemAbstract
    {
        protected readonly EntitiesViewUI UIE;

        protected SystemUIAbstract( in EntitiesViewUI entsUI, in Chessy.Game.Entity.Model.EntitiesModel ents) : base(ents)
        {
            UIE = entsUI;
        }
    }
}