using Photon.Pun;
using Photon.Realtime;
namespace Chessy.Model.System
{
    public partial class SystemsModel : IUpdate
    {
        internal void TryShiftUnitOntoOtherCellM(in byte cellIdxFrom, in byte cellIdxTo, in Player sender)
        {
            //PlayerTypes whoDoing;

            //if (PhotonNetwork.OfflineMode)
            //{
            //    whoDoing = _unitCs[cellIdxFrom].PlayerT;
            //}
            //else
            //{
            //    whoDoing = sender.GetPlayer();
            //}

            var whoDoing = PhotonNetwork.OfflineMode ? unitCs[cellIdxFrom].PlayerT : sender.GetPlayer();

            if (WhereUnitCanShiftC(cellIdxFrom).CanShiftHere(cellIdxTo) && unitCs[cellIdxFrom].PlayerT == whoDoing)
            {
                unitCs[cellIdxFrom].ConditionT = ConditionUnitTypes.None;

                if (shiftingUnitCs[cellIdxFrom].WhereNeedShiftIdxCell != 0)
                {
                    shiftingUnitCs[cellIdxFrom].NeedReturnBack = true;
                }
                else
                {
                    shiftingUnitCs[cellIdxFrom].WhereNeedShiftIdxCell = cellIdxTo;
                }

                if (PhotonNetwork.OfflineMode)
                {
                    if (whoDoing == PlayerTypes.First)
                    {
                        RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.SoundRunningUnit);
                    }
                }
                else
                {
                    RpcSs.ExecuteSoundActionToGeneral(sender, ClipTypes.SoundRunningUnit);
                }

                
            }
        }
    }
}