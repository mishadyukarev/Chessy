using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;
using System;

namespace Chessy.Game.Model.System
{
    sealed partial class ExecuteUpdateEverythingMS : SystemModel
    {
        void TryGiveWaterToUnitsAroundRainy()
        {
            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (_eMG.UnitTC(cell_0).HaveUnit)
                {
                    if (_eMG.UnitT(cell_0) == UnitTypes.Snowy)
                    {
                        if (!_eMG.LessonTC.HaveLesson)
                        {
                            GiveWaterToUnitsAroundRainy(cell_0);
                        }
                    }
                }
            }
        }

        internal void GiveWaterToUnitsAroundRainy(in byte cellIdx)
        {
            foreach (var cell_1 in _eMG.AroundCellsE(cellIdx).CellsAround)
            {
                if (_eMG.UnitTC(cell_1).HaveUnit)
                {
                    if (_eMG.UnitPlayerT(cellIdx) == _eMG.UnitPlayerT(cell_1))
                    {
                        _eMG.WaterUnitC(cell_1).Water = WaterValues.MAX;
                    }
                }
            }
        }
    }
}