using Chessy.Game.Model.Entity;
using Chessy.Game.Values;
using Chessy.Game.Values.Cell.Unit.Stats;

namespace Chessy.Game.Model.System
{
    static partial class ExecuteUpdateEverythingMS
    {
        static void TryGiveWaterToUnitsAroundRainy(this EntitiesModelGame e)
        {
            for (byte cell_0 = 0; cell_0 < StartValues.CELLS; cell_0++)
            {
                if (e.UnitT(cell_0).HaveUnit())
                {
                    if (e.UnitT(cell_0) == UnitTypes.Snowy)
                    {
                        if (!e.LessonT.HaveLesson())
                        {
                            e.GiveWaterToUnitsAroundRainy(cell_0);
                        }
                    }
                }
            }
        }

        internal static void GiveWaterToUnitsAroundRainy(this EntitiesModelGame e, in byte cellIdx)
        {
            foreach (var cellIdxDirect in e.AroundCellsE(cellIdx).CellsAround)
            {
                if (e.UnitT(cellIdxDirect).HaveUnit())
                {
                    if (e.UnitPlayerT(cellIdx) == e.UnitPlayerT(cellIdxDirect))
                    {
                        e.WaterUnitC(cellIdxDirect).Water = WaterValues.MAX;
                    }
                }
            }
        }
    }
}