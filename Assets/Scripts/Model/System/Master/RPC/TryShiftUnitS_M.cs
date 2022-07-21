using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryShiftUnitOntoOtherCellM(in byte cellIdxFrom, in byte cellIdxTo, in Player sender)
        {
            var whoDoing = PhotonNetwork.OfflineMode ? PlayerTypes.First : sender.GetPlayer();

            if (_whereUnitCanShiftCs[cellIdxFrom].CanShiftHere(cellIdxTo) && _unitCs[cellIdxFrom].PlayerT == whoDoing)
            {
                _unitCs[cellIdxFrom].ConditionT = ConditionUnitTypes.None;

                if (_shiftingUnitCs[cellIdxFrom].WhereNeedShiftIdxCell != 0)
                {
                    _shiftingUnitCs[cellIdxFrom].NeedReturnBack = true;
                }
                else
                {
                    _shiftingUnitCs[cellIdxFrom].WhereNeedShiftIdxCell = cellIdxTo;
                }

                RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.SoundRunningUnit);
            }
        }
    }
}