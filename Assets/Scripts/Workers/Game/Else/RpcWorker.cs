using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

namespace Assets.Scripts.Workers
{
    internal class RpcWorker : MainMasterWorker
    {
        internal static PhotonMessageInfo InfoFrom => EGMM.FromInfoEnt_FromInfoCom.FromInfo;

        internal static int[] XyCellForProtectRelax => EGMM.ProtectRelaxEnt_XyCellCom.XyCell;
        internal static ConditionUnitTypes NeededProtectRelaxType => EGMM.ProtectRelaxEnt_ProtectRelaxCom.ProtectRelaxType;
    }
}
