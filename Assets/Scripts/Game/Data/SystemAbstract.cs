namespace Chessy.Game
{
    public abstract class SystemAbstract
    {
        protected readonly Chessy.Game.Entity.Model.EntitiesModel E;
        protected SystemAbstract(in Chessy.Game.Entity.Model.EntitiesModel ents) => E = ents;
    }
}