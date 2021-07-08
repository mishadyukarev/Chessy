using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal class UpgradeMasterSystem : RPCMasterSystemReduction
    {
        private const byte FOR_NEXT_UPGRADE = 1;

        private PhotonMessageInfo Info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;

        private UpgradeModTypes UpgradeModType => _eMM.UpgradeEnt_UpgradeTypeCom.UpgradeModType;
        private int[] XyCellForUpgrade => _eMM.UpgradeEnt_XyCellCom.XyCell;


        private UnitTypes NeededUnitTypeForUpgrade => _eGM.CellUnitEnt_UnitTypeCom(XyCellForUpgrade).UnitType;
        internal BuildingTypes BuildingTypeForUpgrade => _eMM.UpgradeEnt_BuildingTypeCom.BuildingType;


        public override void Run()
        {
            base.Run();

            bool[] haves;

            switch (UpgradeModType)
            {
                case UpgradeModTypes.None:
                    break;

                case UpgradeModTypes.Unit:
                    if (EconomyManager.CanUpgradeUnit(Info.Sender, NeededUnitTypeForUpgrade, out haves))
                    {
                        EconomyManager.UpgradeUnit(Info.Sender, NeededUnitTypeForUpgrade);
                        CellUnitWorker.ChangeUnit(XyCellForUpgrade, NeededUnitTypeForUpgrade + FOR_NEXT_UPGRADE);
                    }
                    else
                    {
                        _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves);
                    }
                    break;

                case UpgradeModTypes.Building:
                    if (EconomyManager.CanUpgradeBuildings(Info.Sender, BuildingTypeForUpgrade, out haves))
                    {
                        EconomyManager.UpgradeBuildings(Info.Sender, BuildingTypeForUpgrade);
                    }
                    else
                    {
                        _photonPunRPC.MistakeEconomyToGeneral(Info.Sender, haves);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
