using ECS;

namespace Game.Game
{
    public sealed class UnitsInInventorE : EntityAbstract
    {
        readonly UnitTypes _unitT;
        readonly LevelTypes _levelT;
        public AmountC AmountC => Ent.Get<AmountC>();

        public bool HaveUnits => AmountC.Amount > 0;

        internal UnitsInInventorE(in UnitTypes unit, in LevelTypes level, in EcsWorld gameW) : base(gameW)
        {
            _unitT = unit;
            _levelT = level;

            Ent.Add(new AmountC(StartValues.Units(unit, level)));
        }
    }
}