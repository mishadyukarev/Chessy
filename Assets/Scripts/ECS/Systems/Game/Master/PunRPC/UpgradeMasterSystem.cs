using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Info;
using Leopotam.Ecs;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal class UpgradeMasterSystem : SystemMasterReduction
    {
        private EcsFilter<XyUnitsComponent> _xyUnitsFilter;

        private const byte FOR_NEXT_UPGRADE = 1;

        private UpgradeModTypes UpgradeModType => _eMM.UpgradeEnt_UpgradeTypeCom.UpgradeModType;
        private int[] XyCellForUpgrade => _eMM.UpgradeEnt_XyCellCom.XyCell;


        private UnitTypes CurrentUnitType => CellUnitsDataSystem.UnitType(XyCellForUpgrade);
        private UnitTypes NeededUnitTypeForUpgrade => CellUnitsDataSystem.UnitType(XyCellForUpgrade) + FOR_NEXT_UPGRADE;
        internal BuildingTypes NeededBuildingTypeForUpgrade => _eMM.UpgradeEnt_BuildingTypeCom.BuildingType;


        public override void Run()
        {
            base.Run();

            ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

            bool[] haves;

            switch (UpgradeModType)
            {
                case UpgradeModTypes.None:
                    break;

                case UpgradeModTypes.Unit:
                    if (CellUnitsDataSystem.HaveAnyUnit(XyCellForUpgrade))
                    {
                        if (CellUnitsDataSystem.HaveOwner(XyCellForUpgrade))
                        {
                            if (CellUnitsDataSystem.IsHim(RpcMasterDataContainer.InfoFrom.Sender, XyCellForUpgrade))
                            {
                                if (ResourcesUIDataContainer.CanUpgradeUnit(RpcMasterDataContainer.InfoFrom.Sender, CurrentUnitType, out haves))
                                {
                                    ResourcesUIDataContainer.BuyUpgradeUnit(RpcMasterDataContainer.InfoFrom.Sender, CurrentUnitType);


                                    var preConditionType = CellUnitsDataSystem.ConditionType(XyCellForUpgrade);
                                    var preUnitType = CellUnitsDataSystem.UnitType(XyCellForUpgrade);
                                    var preKey = CellUnitsDataSystem.IsMasterClient(XyCellForUpgrade);
                                    var preMaxHealth = CellUnitsDataSystem.MaxAmountHealth(preUnitType);

                                    MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(preConditionType, preUnitType, preKey, XyCellForUpgrade);
                                    xyUnitsCom.RemoveAmountUnitsInGame(preUnitType, preKey, XyCellForUpgrade);


                                    CellUnitsDataSystem.SetUnitType(NeededUnitTypeForUpgrade, XyCellForUpgrade);

                                    var newUnitType = CellUnitsDataSystem.UnitType(XyCellForUpgrade);
                                    var newMaxHealth = CellUnitsDataSystem.MaxAmountHealth(newUnitType);

                                    CellUnitsDataSystem.AddAmountHealth(XyCellForUpgrade, newMaxHealth - preMaxHealth);

                                    xyUnitsCom.AddAmountUnitInGame(newUnitType, preKey, XyCellForUpgrade);
                                    MainGameSystem.XyUnitsContitionCom.AddUnitInCondition(preConditionType, newUnitType, preKey, XyCellForUpgrade);


                                    if (CellUnitsDataSystem.IsMelee(XyCellForUpgrade))
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
