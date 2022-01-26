using ECS;

namespace Game.Game
{
    public sealed class CellUnitUniqueButtonsE : EntityAbstract
    {
        public ref UniqueAbilityC AbilityC => ref Ent.Get<UniqueAbilityC>();

        public CellUnitUniqueButtonsE(in EcsWorld gameW) : base(gameW) { }
    }
}