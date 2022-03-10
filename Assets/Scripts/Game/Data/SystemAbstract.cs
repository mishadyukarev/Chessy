namespace Chessy.Game
{
    public abstract class SystemAbstract
    {
        protected readonly EntitiesModel E;
        protected SystemAbstract(in EntitiesModel ents) => E = ents;
    }
}