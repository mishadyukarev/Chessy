using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryShiftUnitOntoOtherCellM(in byte cellIdxFrom, in byte cellIdxTo, in Player sender)
        {
            var whoDoing = PhotonNetwork.OfflineMode ? PlayerTypes.First : sender.GetPlayer();

            if (_e.WhereUnitCanShiftC(cellIdxFrom).CanShiftHere(cellIdxTo) && _e.UnitPlayerT(cellIdxFrom).Is(whoDoing))
            {
                _e.SetUnitConditionT(cellIdxFrom, ConditionUnitTypes.None);

                if (_e.ShiftingInfoForUnitC(cellIdxFrom).WhereNeedShiftIdxCell != 0)
                {
                    _e.ShiftingInfoForUnitC(cellIdxFrom).NeedReturnBack = true;
                }
                else
                {
                    _e.ShiftingInfoForUnitC(cellIdxFrom).WhereNeedShiftIdxCell = cellIdxTo;
                }

                RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.SoundRunningUnit);
            }
        }
    }
}