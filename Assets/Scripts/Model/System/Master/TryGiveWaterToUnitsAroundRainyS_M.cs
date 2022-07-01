using Chessy.Model.Values;
namespace Chessy.Model.System
{
    sealed partial class ExecuteUpdateEverythingMS
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
                            _s.GiveWaterToUnitsAroundRainy(cell_0);
                        }
                    }
                }
            }
        }
    }
}