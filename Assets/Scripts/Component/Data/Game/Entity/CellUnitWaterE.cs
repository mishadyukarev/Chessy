using ECS;

namespace Game.Game
{
    public sealed class CellUnitWaterE : EntityAbstract
    {
        public ref AmountC AmountC => ref Ent.Get<AmountC>();

        public CellUnitWaterE(in EcsWorld gameW) : base(gameW) { }
    }
}