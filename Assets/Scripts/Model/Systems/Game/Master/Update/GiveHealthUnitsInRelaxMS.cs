using Chessy.Game.Model.Entity;
using Chessy.Game.Model.System;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game
{
    sealed class GiveHealthUnitsInRelaxMS : SystemModel
    {
        internal GiveHealthUnitsInRelaxMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void GiveHealth()
        {
            for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
            {
                if (eMG.UnitConditionTC(cellIdx0).Is(ConditionUnitTypes.Relaxed))
                {
                    eMG.HpUnitC(cellIdx0).Health = HpValues.MAX;
                }
            }
        }
    }
}