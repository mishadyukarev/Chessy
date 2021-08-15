using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Game;
using Assets.Scripts.ECS.Component.Game.Master;
using Leopotam.Ecs;
using System;

namespace Assets.Scripts.ECS.Game.Master.Systems.PunRPC
{
    internal class UpgradeMasterSystem : IEcsRunSystem
    {
        private EcsFilter<InfoMasCom> _infoFilter = default;
        private EcsFilter<ForUpgradeMasCom> _forUpgradeFilter = default;

        private EcsFilter<UpgradesBuildingsComponent> _upgradeBuildsFilter = default;
        private EcsFilter<UnitsInGameInfoComponent> _unitsInGameFilter = default;
        private EcsFilter<UnitsInConditionInGameCom> _unitsInCondFilter = default;
        private EcsFilter<InventorResourcesComponent> _inventResFilt = default;

        private EcsFilter<CellUnitDataComponent, OwnerComponent> _cellUnitFilter = default;

        private const byte FOR_NEXT_UPGRADE = 1;

        public void Run()
        {
            ref var infoCom = ref _infoFilter.Get1(0);
            var forUpgradeCom = _forUpgradeFilter.Get1(0);

            var sender = infoCom.FromInfo.Sender;
            var idxForUpgradeUnit = forUpgradeCom.IdxForUpgradeUnit;

            ref var curCellUnitDataCom = ref _cellUnitFilter.Get1(idxForUpgradeUnit);
            ref var curOwnerCellUnitDataCom = ref _cellUnitFilter.Get2(idxForUpgradeUnit);

            ref var unitsInGameCom = ref _unitsInGameFilter.Get1(0);
            ref var unitsInCondCom = ref _unitsInCondFilter.Get1(0);
            ref var inventResCom = ref _inventResFilt.Get1(0);



            bool[] haves;

            switch (forUpgradeCom.UpgradeModType)
            {
                case UpgradeModTypes.None:
                    throw new Exception();

                case UpgradeModTypes.Unit:
                    if (curCellUnitDataCom.HaveUnit)
                    {
                        if (curOwnerCellUnitDataCom.HaveOwner)
                        {
                            if (curOwnerCellUnitDataCom.IsHim(sender))
                            {
                                if (inventResCom.CanUpgradeUnit(sender, curCellUnitDataCom.UnitType, out haves))
                                {
                                    inventResCom.BuyUpgradeUnit(sender, curCellUnitDataCom.UnitType);


                                    var preConditionType = curCellUnitDataCom.ConditionUnitType;
                                    var preUnitType = curCellUnitDataCom.UnitType;
                                    var preKey = curOwnerCellUnitDataCom.IsMasterClient;
                                    var preMaxHealth = curCellUnitDataCom.MaxAmountHealth;

                                    unitsInCondCom.RemoveUnitInCondition(preConditionType, preUnitType, preKey, idxForUpgradeUnit);
                                    unitsInGameCom.RemoveAmountUnitsInGame(preUnitType, preKey, idxForUpgradeUnit);

                                    curCellUnitDataCom.UnitType = curCellUnitDataCom.UnitType + FOR_NEXT_UPGRADE;
                                    //switch (curCellUnitDataCom.UnitType)
                                    //{
                                    //    case UnitTypes.None:
                                    //        throw new Exception();

                                    //    case UnitTypes.King:
                                    //        throw new Exception();

                                    //    case UnitTypes.Pawn:
                                    //        throw new Exception();

                                    //    case UnitTypes.Rook:
                                    //        throw new Exception();

                                    //    case UnitTypes.Rook_Crossbow:
                                    //        curCellUnitDataCom.AmountHealth += curCellUnitDataCom.MaxAmountHealth - UnitValues.STANDART_AMOUNT_HEALTH_ROOK_CROSSBOW;
                                    //        break;

                                    //    case UnitTypes.Bishop:
                                    //        throw new Exception();

                                    //    case UnitTypes.Bishop_Crossbow:
                                    //        curCellUnitDataCom.AmountHealth += curCellUnitDataCom.MaxAmountHealth - UnitValues.STANDART_AMOUNT_HEALTH_BISHOP_CROSSBOW;
                                    //        break;

                                    //    default:
                                    //        throw new Exception();
                                    //}


                                    var newUnitType = curCellUnitDataCom.UnitType;
                                    var newMaxHealth = curCellUnitDataCom.MaxAmountHealth;

                                    //curCellUnitDataCom.AddAmountHealth(newMaxHealth - preMaxHealth);

                                    unitsInGameCom.AddAmountUnitInGame(newUnitType, preKey, idxForUpgradeUnit);
                                    unitsInCondCom.AddUnitInCondition(preConditionType, newUnitType, preKey, idxForUpgradeUnit);


                                    if (curCellUnitDataCom.IsMelee)
                                    {
                                        RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.UpgradeUnitMelee);
                                    }
                                    else
                                    {
                                        RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.UpgradeUnitArcher);
                                    }
                                }
                                else
                                {
                                    RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                                    RPCGameSystem.MistakeEconomyToGeneral(sender, haves);
                                }
                            }
                        }
                    }
                    break;

                case UpgradeModTypes.Building:

                    var buildTypeForUpgrade = _forUpgradeFilter.Get1(0).BuildingType;

                    if (inventResCom.CanUpgradeBuildings(sender, buildTypeForUpgrade, out haves))
                    {
                        inventResCom.BuyUpgradeBuildings(sender, buildTypeForUpgrade);
                        _upgradeBuildsFilter.Get1(0).AddAmountUpgrades(buildTypeForUpgrade, sender.IsMasterClient);

                        RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.SoundGoldPack);
                    }
                    else
                    {
                        RPCGameSystem.SoundToGeneral(sender, SoundEffectTypes.Mistake);
                        RPCGameSystem.MistakeEconomyToGeneral(sender, haves);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
