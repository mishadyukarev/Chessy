//using Assets.Scripts.Abstractions.Enums;
//using Assets.Scripts.ECS.Systems.Game.Master;
//using Photon.Pun;

//namespace Assets.Scripts.Workers
//{
//    internal class RpcMasterDataContainer
//    {
//        private static GameMasterSystemManager EGMM => Main.Instance.ECSmanager.GameMasterSystemManager;

//        internal static PhotonMessageInfo InfoFrom
//        {
//            get => MainMasterSystem.FromInfoEnt_FromInfoCom.FromInfo;
//            set => MainMasterSystem.FromInfoEnt_FromInfoCom.FromInfo = value;
//        }

//        internal static int[] XyCellForCondition => MainMasterSystem.ProtectRelaxEnt_XyCellCom.XyCell;
//        internal static ConditionUnitTypes NeededConditionType => MainMasterSystem.ProtectRelaxEnt_ProtectRelaxCom.ProtectRelaxType;

//        internal static int[] XyCellForCircularAttack
//        {
//            get => MainMasterSystem.CircularAttackKingEnt_XyCellCom.XyCell;
//            set => MainMasterSystem.CircularAttackKingEnt_XyCellCom.XyCell = value;
//        }
//    }
//}
