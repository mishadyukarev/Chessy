using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

namespace Assets.Scripts.Workers
{
    internal class RpcWorker : MainMasterWorker
    {
        internal static PhotonMessageInfo InfoFrom => EGMM.FromInfoEnt_FromInfoCom.FromInfo;

        internal static int[] XyCellForCondition => EGMM.ProtectRelaxEnt_XyCellCom.XyCell;
        internal static ConditionUnitTypes NeededConditionType => EGMM.ProtectRelaxEnt_ProtectRelaxCom.ProtectRelaxType;
    }
}
