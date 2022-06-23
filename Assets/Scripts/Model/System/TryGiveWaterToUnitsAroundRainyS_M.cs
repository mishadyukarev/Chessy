using Chessy.Model.Model.Entity;
using Chessy.Model.Values;
using Chessy.Model.Values.Cell.Unit.Stats;

namespace Chessy.Model.Model.System
{
    static partial class ExecuteUpdateEverythingMS
    {
        static void TryGiveWaterToUnitsAroundRainy(this EntitiesModel e)
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

        internal static void GiveWaterToUnitsAroundRainy(this EntitiesModel e, in byte cellIdx)
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