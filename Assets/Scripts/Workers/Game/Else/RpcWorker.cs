using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

namespace Assets.Scripts.Workers
{
    internal class RpcWorker : MainMasterWorker
    {
        internal static PhotonMessageInfo InfoFrom => EGMM.FromInfoEnt_FromInfoCom.InfoFrom;

        internal static int[] XyCellForProtectRelax => EGMM.ProtectRelaxEnt_XyCellCom.XyCell;
        internal static ProtectRelaxTypes NeededProtectRelaxType => EGMM.ProtectRelaxEnt_ProtectRelaxCom.ProtectRelaxType;
    }
}
