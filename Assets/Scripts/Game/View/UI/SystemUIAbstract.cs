namespace Chessy.Game
{
    public abstract class SystemUIAbstract : SystemAbstract
    {
        protected readonly EntitiesViewUIGame UIE;

        protected SystemUIAbstract( in EntitiesViewUIGame entsUI, in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
            UIE = entsUI;
        }
    }
}