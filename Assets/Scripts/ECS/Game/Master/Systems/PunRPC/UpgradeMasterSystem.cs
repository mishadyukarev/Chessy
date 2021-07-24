using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Info;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal class UpgradeMasterSystem : RPCMasterSystemReduction
    {
        private const byte FOR_NEXT_UPGRADE = 1;

        private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

        private UpgradeModTypes UpgradeModType => _eMM.UpgradeEnt_UpgradeTypeCom.UpgradeModType;
        private int[] XyCellForUpgrade => _eMM.UpgradeEnt_XyCellCom.XyCell;


        private UnitTypes NeededUnitTypeForUpgrade => CellUnitWorker.UnitType(XyCellForUpgrade);
        internal BuildingTypes NeededBuildingTypeForUpgrade => _eMM.UpgradeEnt_BuildingTypeCom.BuildingType;


        public override void Run()
        {
            base.Run();

            bool[] haves;

            switch (UpgradeModType)
            {
                case UpgradeModTypes.None:
                    break;

                case UpgradeModTypes.Unit:
                    if (CellUnitWorker.HaveAnyUnit(XyCellForUpgrade))
                    {
                        if (InfoResourcesWorker.CanUpgradeUnit(InfoFrom.Sender, NeededUnitTypeForUpgrade, out haves))
                        {
                            InfoResourcesWorker.UpgradeUnit(InfoFrom.Sender, NeededUnitTypeForUpgrade);
                            CellUnitWorker.ChangePlayerUnit(XyCellForUpgrade, NeededUnitTypeForUpgrade + FOR_NEXT_UPGRADE);

                            if (CellUnitWorker.IsMelee(XyCellForUpgrade))
                            {
                                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.UpgradeUnitMelee);
                            }
                            else
                            {
                                PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.UpgradeUnitArcher);
                            }
                        }
                        else
                        {
                            PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                            PhotonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
                        }
                    }
                    break;

                case UpgradeModTypes.Building:
                    if (InfoResourcesWorker.CanUpgradeBuildings(InfoFrom.Sender, NeededBuildingTypeForUpgrade, out haves))
                    {
                        InfoResourcesWorker.UpgradeBuildings(InfoFrom.Sender, NeededBuildingTypeForUpgrade);

                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.SoundGoldPack);
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(InfoFrom.Sender, SoundEffectTypes.Mistake);
                        PhotonPunRPC.MistakeEconomyToGeneral(InfoFrom.Sender, haves);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
