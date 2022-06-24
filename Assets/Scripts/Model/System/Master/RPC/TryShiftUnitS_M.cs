using Photon.Realtime;

namespace Chessy.Model.Model.System
{
    public sealed partial class SystemsModel : IUpdate
    {
        internal void TryShiftUnitOntoOtherCellM(in byte cell_from, in byte cell_to, in Player sender)
        {
            if (_e.WhereUnitCanShiftC(cell_from).CanShiftHere(cell_to) && _e.UnitPlayerT(cell_from).Is(_e.WhoseMovePlayerT))
            {
                _e.EnergyUnitC(cell_from).Energy -= _e.HowManyEnergyNeedForShiftingUnitC(cell_from).HowManyEnergyNeedForShiftingToHere(cell_to);


                ShiftUnitOnOtherCellM(cell_from, cell_to);

                ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}