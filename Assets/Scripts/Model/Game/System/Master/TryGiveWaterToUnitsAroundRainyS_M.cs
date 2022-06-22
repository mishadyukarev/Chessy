using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.Model.System
{
    sealed partial class ExecuteUpdateEverythingMS : SystemModel
    {
        void TryGiveWaterToUnitsAroundRainy()
        {
            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (_e.UnitT(cell_0).HaveUnit())
                {
                    if (_e.UnitT(cell_0) == UnitTypes.Snowy)
                    {
                        if (!_e.LessonT.HaveLesson())
                        {
                            GiveWaterToUnitsAroundRainy(cell_0);
                        }
                    }
                }
            }
        }

        internal void GiveWaterToUnitsAroundRainy(in byte cellIdx)
        {
            foreach (var cellIdxDirect in _e.AroundCellsE(cellIdx).CellsAround)
            {
                if (_e.UnitT(cellIdxDirect).HaveUnit())
                {
                    if (_e.UnitPlayerT(cellIdx) == _e.UnitPlayerT(cellIdxDirect))
                    {
                        _e.WaterUnitC(cellIdxDirect).Water = WaterValues.MAX;
                    }
                }
            }
        }
    }
}