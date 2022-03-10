namespace Chessy.Game.System.Model
{
    public abstract class CellSystem : SystemAbstract
    {
        protected readonly byte Idx;

        protected CellSystem(in byte idx, in EntitiesModel eM) : base(eM) => Idx = idx;
    }
}