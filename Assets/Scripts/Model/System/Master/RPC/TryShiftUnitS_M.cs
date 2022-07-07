using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryShiftUnitOntoOtherCellM(in byte cellIdxFrom, in byte cellIdxTo, in Player sender)
        {
            var whoDoing = PhotonNetwork.OfflineMode ? PlayerTypes.First : sender.GetPlayer();

            if(_e.UnitT(cellIdxFrom) == UnitTypes.Pawn)
            {

            }

            if (_e.WhereUnitCanShiftC(cellIdxFrom).Can(cellIdxTo) && _e.UnitPlayerT(cellIdxFrom).Is(whoDoing))
            {
                _e.SetUnitConditionT(cellIdxFrom, ConditionUnitTypes.None);

                if (_e.UnitMainC(cellIdxFrom).IdxWhereNeedShiftUnitOnOtherCell != 0)
                {
                    _e.UnitMainC(cellIdxFrom).NeedToBackUnitOnHisCell = true;
                }
                else
                {
                    _e.UnitMainC(cellIdxFrom).IdxWhereNeedShiftUnitOnOtherCell = cellIdxTo;
                }

                GetDataCellsS.GetDataCellsM();

                _e.SoundAction(ClipTypes.SoundRunningUnit).Invoke();
            }
        }
    }
}