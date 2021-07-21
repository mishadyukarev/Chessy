using Assets.Scripts.Abstractions.Enums;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal class UpgradeMasterSystem : RPCMasterSystemReduction
    {
        private const byte FOR_NEXT_UPGRADE = 1;

        private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.InfoFrom;

        private UpgradeModTypes UpgradeModType => _eMM.UpgradeEnt_UpgradeTypeCom.UpgradeModType;
        private int[] XyCellForUpgrade => _eMM.UpgradeEnt_XyCellCom.XyCell;


        private UnitTypes NeededUnitTypeForUpgrade => _eGM.CellUnitEnt_UnitTypeCom(XyCellForUpgrade).UnitType;
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
                    if (_eGM.CellUnitEnt_UnitTypeCom(XyCellForUpgrade).HaveAnyUnit)
                    {
                        if (EconomyWorker.CanUpgradeUnit(InfoFrom.Sender, NeededUnitTypeForUpgrade, out haves))
                        {
                            EconomyWorker.UpgradeUnit(InfoFrom.Sender, NeededUnitTypeForUpgrade);
                            CellUnitWorker.ChangePlayerUnit(XyCellForUpgrade, NeededUnitTypeForUpgrade + FOR_NEXT_UPGRADE);

                            if (_eGM.CellUnitEnt_UnitTypeCom(XyCellForUpgrade).IsMelee)
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
                    if (EconomyWorker.CanUpgradeBuildings(InfoFrom.Sender, NeededBuildingTypeForUpgrade, out haves))
                    {
                        EconomyWorker.UpgradeBuildings(InfoFrom.Sender, NeededBuildingTypeForUpgrade);

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
