using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Info;
using Photon.Pun;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal class UpgradeMasterSystem : RPCMasterSystemReduction
    {
        private const byte FOR_NEXT_UPGRADE = 1;

        private PhotonMessageInfo InfoFrom => _eMM.FromInfoEnt_FromInfoCom.FromInfo;

        private UpgradeModTypes UpgradeModType => _eMM.UpgradeEnt_UpgradeTypeCom.UpgradeModType;
        private int[] XyCellForUpgrade => _eMM.UpgradeEnt_XyCellCom.XyCell;


        private UnitTypes CurrentUnitType => CellUnitsDataWorker.UnitType(XyCellForUpgrade);
        private UnitTypes NeededUnitTypeForUpgrade => CellUnitsDataWorker.UnitType(XyCellForUpgrade) + FOR_NEXT_UPGRADE;
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
                    if (CellUnitsDataWorker.HaveAnyUnit(XyCellForUpgrade))
                    {
                        if (InfoResourcesDataWorker.CanUpgradeUnit(InfoFrom.Sender, CurrentUnitType, out haves))
                        {
                            InfoResourcesDataWorker.BuyUpgradeUnit(InfoFrom.Sender, CurrentUnitType);


                            var preConditionType = CellUnitsDataWorker.ConditionType(XyCellForUpgrade);
                            var preUnitType = CellUnitsDataWorker.UnitType(XyCellForUpgrade);
                            var preKey = CellUnitsDataWorker.IsMasterClient(XyCellForUpgrade);
                            var preMaxHealth = CellUnitsDataWorker.MaxAmountHealth(preUnitType);

                            InfoUnitsConditionWorker.RemoveUnitInCondition(preConditionType, preUnitType, preKey, XyCellForUpgrade);
                            InfoAmountUnitsWorker.RemoveAmountUnitsInGame(preUnitType, preKey, XyCellForUpgrade);


                            CellUnitsDataWorker.SetUnitType(NeededUnitTypeForUpgrade, XyCellForUpgrade);

                            var newUnitType = CellUnitsDataWorker.UnitType(XyCellForUpgrade);
                            var newMaxHealth = CellUnitsDataWorker.MaxAmountHealth(newUnitType);

                            CellUnitsDataWorker.AddAmountHealth(XyCellForUpgrade, newMaxHealth - preMaxHealth);

                            InfoAmountUnitsWorker.AddAmountUnitInGame(newUnitType, preKey, XyCellForUpgrade);
                            InfoUnitsConditionWorker.AddUnitInCondition(preConditionType, newUnitType, preKey, XyCellForUpgrade);


                            if (CellUnitsDataWorker.IsMelee(XyCellForUpgrade))
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
                    if (InfoResourcesDataWorker.CanUpgradeBuildings(InfoFrom.Sender, NeededBuildingTypeForUpgrade, out haves))
                    {
                        InfoResourcesDataWorker.BuyUpgradeBuildings(InfoFrom.Sender, NeededBuildingTypeForUpgrade);

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
