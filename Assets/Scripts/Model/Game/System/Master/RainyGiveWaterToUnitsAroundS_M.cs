using Chessy.Common.Entity;
using Chessy.Common.Model.System;
using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.Model.System
{
    sealed class RainyGiveWaterToUnitsAroundS_M : SystemModel
    {
        internal RainyGiveWaterToUnitsAroundS_M(in SystemsModelGame sMG, in EntitiesModelGame eMG) : base(sMG, eMG)
        {
        }

        internal void Give(in byte cell_0)
        {
            if (!eMG.UnitTC(cell_0).Is(UnitTypes.Snowy)) throw new Exception();


            foreach (var cell_1 in eMG.AroundCellsE(cell_0).CellsAround)
            {
                if (eMG.UnitTC(cell_1).HaveUnit)
                {
                    if (eMG.UnitPlayerT(cell_0) == eMG.UnitPlayerT(cell_1))
                    {
                        eMG.WaterUnitC(cell_1).Water = WaterValues.MAX;
                    }
                }

            }
        }
    }
}