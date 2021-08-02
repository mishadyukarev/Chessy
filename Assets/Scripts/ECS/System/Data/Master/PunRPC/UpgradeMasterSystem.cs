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


        private UnitTypes CurrentUnitType => CellUnitsDataContainer.UnitType(XyCellForUpgrade);
        private UnitTypes NeededUnitTypeForUpgrade => CellUnitsDataContainer.UnitType(XyCellForUpgrade) + FOR_NEXT_UPGRADE;
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
                    if (CellUnitsDataContainer.HaveAnyUnit(XyCellForUpgrade))
                    {
                        if (CellUnitsDataContainer.HaveOwner(XyCellForUpgrade))
                        {
                            if (CellUnitsDataContainer.IsHim(RpcMasterDataContainer.InfoFrom.Sender, XyCellForUpgrade))
                            {
                                if (ResourcesUIDataContainer.CanUpgradeUnit(RpcMasterDataContainer.InfoFrom.Sender, CurrentUnitType, out haves))
                                {
                                    ResourcesUIDataContainer.BuyUpgradeUnit(RpcMasterDataContainer.InfoFrom.Sender, CurrentUnitType);


                                    var preConditionType = CellUnitsDataContainer.ConditionType(XyCellForUpgrade);
                                    var preUnitType = CellUnitsDataContainer.UnitType(XyCellForUpgrade);
                                    var preKey = CellUnitsDataContainer.IsMasterClient(XyCellForUpgrade);
                                    var preMaxHealth = CellUnitsDataContainer.MaxAmountHealth(preUnitType);

                                    InfoUnitsDataContainer.RemoveUnitInCondition(preConditionType, preUnitType, preKey, XyCellForUpgrade);
                                    InfoUnitsDataContainer.RemoveAmountUnitsInGame(preUnitType, preKey, XyCellForUpgrade);


                                    CellUnitsDataContainer.SetUnitType(NeededUnitTypeForUpgrade, XyCellForUpgrade);

                                    var newUnitType = CellUnitsDataContainer.UnitType(XyCellForUpgrade);
                                    var newMaxHealth = CellUnitsDataContainer.MaxAmountHealth(newUnitType);

                                    CellUnitsDataContainer.AddAmountHealth(XyCellForUpgrade, newMaxHealth - preMaxHealth);

                                    InfoUnitsDataContainer.AddAmountUnitInGame(newUnitType, preKey, XyCellForUpgrade);
                                    InfoUnitsDataContainer.AddUnitInCondition(preConditionType, newUnitType, preKey, XyCellForUpgrade);


                                    if (CellUnitsDataContainer.IsMelee(XyCellForUpgrade))
                                    {
                                        PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.UpgradeUnitMelee);
                                    }
                                    else
                                    {
                                        PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.UpgradeUnitArcher);
                                    }
                                }
                                else
                                {
                                    PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                                    PhotonPunRPC.MistakeEconomyToGeneral(RpcMasterDataContainer.InfoFrom.Sender, haves);
                                }
                            }
                        }
                    }
                    break;

                case UpgradeModTypes.Building:
                    if (ResourcesUIDataContainer.CanUpgradeBuildings(RpcMasterDataContainer.InfoFrom.Sender, NeededBuildingTypeForUpgrade, out haves))
                    {
                        ResourcesUIDataContainer.BuyUpgradeBuildings(RpcMasterDataContainer.InfoFrom.Sender, NeededBuildingTypeForUpgrade);

                        PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.SoundGoldPack);
                    }
                    else
                    {
                        PhotonPunRPC.SoundToGeneral(RpcMasterDataContainer.InfoFrom.Sender, SoundEffectTypes.Mistake);
                        PhotonPunRPC.MistakeEconomyToGeneral(RpcMasterDataContainer.InfoFrom.Sender, haves);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
