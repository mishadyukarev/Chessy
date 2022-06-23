using Photon.Realtime;

namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel : IUpdate
    {
        internal void TryShiftUnitOntoOtherCellM(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (_e.CellsForShift(cell_from).Contains(cell_to) && _e.UnitPlayerT(cell_from).Is(_e.WhoseMovePlayerT))
            {
                _e.StepUnitC(cell_from).Steps -= _e.UnitNeedStepsForShiftC(cell_from).NeedSteps(cell_to);


                ShiftUnitOnOtherCellM(cell_from, cell_to);

                ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}