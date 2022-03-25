using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    sealed class UpdateHealingUnitMS : SystemModelGameAbs, IEcsRunSystem
    {
        internal UpdateHealingUnitMS(in Chessy.Game.Entity.Model.EntitiesModelGame ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte cell_0 = 0; cell_0 < eMGame.LengthCells; cell_0++)
            {
                if (eMGame.UnitConditionTC(cell_0).Is(ConditionUnitTypes.Relaxed))
                {
                    eMGame.UnitHpC(cell_0).Health = HpValues.MAX;
                }
            }
        }
    }
}