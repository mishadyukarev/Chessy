using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Info;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal class UpgradeMasterSystem : SystemMasterReduction
    {
        private const byte FOR_NEXT_UPGRADE = 1;

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
                        if (CellUnitsDataWorker.HaveOwner(XyCellForUpgrade))
                        {
                            if (CellUnitsDataWorker.IsHim(RpcWorker.InfoFrom.Sender, XyCellForUpgrade))
                            {
                                if (ResourcesDataUIWorker.CanUpgradeUnit(RpcWorker.InfoFrom.Sender, CurrentUnitType, out haves))
                                {
                                    ResourcesDataUIWorker.BuyUpgradeUnit(RpcWorker.InfoFrom.Sender, CurrentUnitType);


                                    var preConditionType = CellUnitsDataWorker.ConditionType(XyCellForUpgrade);
                                    var preUnitType = CellUnitsDataWorker.UnitType(XyCellForUpgrade);
                                    var preKey = CellUnitsDataWorker.IsMasterClient(XyCellForUpgrade);
                                    var preMaxHealth = CellUnitsDataWorker.MaxAmountHealth(preUnitType);

                                    InfoUnitsContainer.RemoveUnitInCondition(preConditionType, preUnitType, preKey, XyCellForUpgrade);
                                    InfoUnitsContainer.RemoveAmountUnitsInGame(preUnitType, preKey, XyCellForUpgrade);


                                    CellUnitsDataWorker.SetUnitType(NeededUnitTypeForUpgrade, XyCellForUpgrade);

                                    var newUnitType = CellUnitsDataWorker.UnitType(XyCellForUpgrade);
                                    var newMaxHealth = CellUnitsDataWorker.MaxAmountHealth(newUnitType);

                                    CellUnitsDataWorker.AddAmountHealth(XyCellForUpgrade, newMaxHealth - preMaxHealth);

                                    InfoUnitsContainer.AddAmountUnitInGame(newUnitType, preKey, XyCellForUpgrade);
                                    InfoUnitsContainer.AddUnitInCondition(preConditionType, newUnitType, preKey, XyCellForUpgrade);


                                    if (CellUnitsDataWorker.IsMelee(XyCellForUpgrade))
                                    {
                                        PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.UpgradeUnitMelee);
                                    }
                                    else
                                    {
                                        PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.UpgradeUnitArcher);
                                    }
                                }
                                else
                                {
                                    PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.Mistake);
                                    PhotonPunRPC.MistakeEconomyToGeneral(RpcWorker.InfoFrom.Sender, haves);
                                }
                            }
                        }
                    }
                    break;

                case UpgradeModTypes.Building:
                    if (ResourcesDataUIWorker.CanUpgradeBuildings(RpcWorker.InfoFrom.Sender, NeededBuildingTypeForUpgrade, out haves))
                    {
                        ResourcesDataUIWorker.BuyUpgradeBuildings(RpcWorker.InfoFrom.Sender, NeededBuildingTypeForUpgrade);

                        PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.SoundGoldPack);
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(RpcWorker.InfoFrom.Sender, SoundEffectTypes.Mistake);
                        PhotonPunRPC.MistakeEconomyToGeneral(RpcWorker.InfoFrom.Sender, haves);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
