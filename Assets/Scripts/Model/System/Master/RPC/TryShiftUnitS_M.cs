using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryShiftUnitOntoOtherCellM(in byte cellIdxFrom, in byte cellIdxTo, in Player sender)
        {
            if (_e.WhereUnitCanShiftC(cellIdxFrom).CanShiftHere(cellIdxTo) && _e.UnitPlayerT(cellIdxFrom).Is(_e.WhoseMovePlayerT))
            {
                //_e.EnergyUnitC(cell_from).Energy -= _e.HowManyEnergyNeedForShiftingUnitC(cell_from).HowManyEnergyNeedForShiftingToHere(cell_to);

                _e.SetUnitConditionT(cellIdxFrom, ConditionUnitTypes.None);

                _e.UnitMainC(cellIdxFrom).IdxWhereNeedShiftUnitOnOtherCell = cellIdxTo;

                _e.SoundAction(ClipTypes.SoundRunningUnit).Invoke();
                //ShiftUnitOnOtherCellM(cellIdxFrom, cellIdxTo);

                //RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.ClickToTable);
            }
        }
    }
}