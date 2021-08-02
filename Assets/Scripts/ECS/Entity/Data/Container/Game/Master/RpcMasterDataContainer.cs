using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

namespace Assets.Scripts.Workers
{
    internal class RpcMasterDataContainer
    {
        private static EntDataGameMasterElseManager EGMM => Main.Instance.ECSmanager.EntDataGameMasterElseManager;

        internal static PhotonMessageInfo InfoFrom
        {
            get => EGMM.FromInfoEnt_FromInfoCom.FromInfo;
            set => EGMM.FromInfoEnt_FromInfoCom.FromInfo = value;
        }

        internal static int[] XyCellForCondition => EGMM.ProtectRelaxEnt_XyCellCom.XyCell;
        internal static ConditionUnitTypes NeededConditionType => EGMM.ProtectRelaxEnt_ProtectRelaxCom.ProtectRelaxType;

        internal static int[] XyCellForCircularAttack
        {
            get => EGMM.CircularAttackKingEnt_XyCellCom.XyCell;
            set => EGMM.CircularAttackKingEnt_XyCellCom.XyCell = value;
        }
    }
}
