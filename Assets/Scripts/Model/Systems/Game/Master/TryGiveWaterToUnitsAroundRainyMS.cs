using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.Model.System
{
    sealed class TryGiveWaterToUnitsAroundRainyMS : SystemModel
    {
        internal TryGiveWaterToUnitsAroundRainyMS(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void TryGive()
        {
            for (byte cellIdx0 = 0; cellIdx0 < StartValues.CELLS; cellIdx0++)
            {
                TryGive(cellIdx0);
            }
        }

        internal void TryGive(in byte idxCell)
        {
            if (eMG.UnitTC(idxCell).HaveUnit)
            {
                if (eMG.UnitT(idxCell) == UnitTypes.Snowy)
                {
                    if (!eMG.LessonTC.HaveLesson)
                    {
                        if (!eMG.UnitTC(idxCell).Is(UnitTypes.Snowy)) throw new Exception();


                        foreach (var cell_1 in eMG.AroundCellsE(idxCell).CellsAround)
                        {
                            if (eMG.UnitTC(cell_1).HaveUnit)
                            {
                                if (eMG.UnitPlayerT(idxCell) == eMG.UnitPlayerT(cell_1))
                                {
                                    eMG.WaterUnitC(cell_1).Water = WaterValues.MAX;
                                }
                            }

                        }
                    }
                }
            }
        }
    }
}