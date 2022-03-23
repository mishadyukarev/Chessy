namespace Chessy.Game
{
    public abstract class SystemAbstract
    {
        protected readonly Chessy.Game.Entity.Model.EntitiesModelGame E;
        protected SystemAbstract(in Chessy.Game.Entity.Model.EntitiesModelGame ents) => E = ents;
    }
}