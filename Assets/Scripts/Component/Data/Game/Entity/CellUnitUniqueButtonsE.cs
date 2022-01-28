using ECS;

namespace Game.Game
{
    public sealed class CellUnitUniqueButtonsE : EntityAbstract
    {
        public ref AbilityC AbilityC => ref Ent.Get<AbilityC>();

        public CellUnitUniqueButtonsE(in EcsWorld gameW) : base(gameW) { }
    }
}