using Photon.Realtime;

namespace Chessy.Game.Model.System
{
    public sealed partial class SystemsModelGame : IUpdate
    {
        internal void TryShiftUnitM(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (_eMG.CellsForShift(cell_from).Contains(cell_to) && _eMG.UnitPlayerTC(cell_from).Is(_eMG.WhoseMovePlayerT))
            {
                _eMG.StepUnitC(cell_from).Steps -= _eMG.UnitNeedStepsForShiftC(cell_from).NeedSteps(cell_to);


                ShiftUnitOnOtherCellM(cell_from, cell_to);

                ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}