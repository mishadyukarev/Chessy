using ECS;

namespace Game.Game
{
    public sealed class CellUnitDefendEffectE : EntityAbstract
    {
        public ref AmountC AmountC => ref Ent.Get<AmountC>();

        public CellUnitDefendEffectE(in EcsWorld gameW) : base(gameW) { }
    }
}