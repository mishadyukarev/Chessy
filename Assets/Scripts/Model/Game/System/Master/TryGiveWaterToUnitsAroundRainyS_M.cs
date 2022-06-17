using Chessy.Game.Model.Entity;
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void TryGiveWaterToUnitsAroundRainyM(in byte cell_0)
        {
            if (!_eMG.UnitTC(cell_0).Is(UnitTypes.Snowy)) throw new Exception();


            foreach (var cell_1 in _eMG.AroundCellsE(cell_0).CellsAround)
            {
                if (_eMG.UnitTC(cell_1).HaveUnit)
                {
                    if (_eMG.UnitPlayerT(cell_0) == _eMG.UnitPlayerT(cell_1))
                    {
                        _eMG.WaterUnitC(cell_1).Water = WaterValues.MAX;
                    }
                }

            }
        }
    }
}