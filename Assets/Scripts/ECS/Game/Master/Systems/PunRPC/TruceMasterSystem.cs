using Assets.Scripts;
using Assets.Scripts.Abstractions.Enums;
using Assets.Scripts.Static;
using Photon.Pun;
using UnityEngine;
using static Assets.Scripts.Main;

internal sealed class TruceMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo Info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;

    public override void Run()
    {
        base.Run();

        if(Instance.GameModeType != GameModTypes.WithBot)
        {
            _photonPunRPC.TruceToGeneral(Info.Sender, false, _eGM.RpcGeneralEnt_RPCCom.NeedActiveSomething, _eGM.MotionEnt_AmountCom.Amount);


            _eGM.TruceEnt_ActivatedDictCom.SetIsActivated(Info.Sender.IsMasterClient, _eGM.RpcGeneralEnt_RPCCom.NeedActiveSomething);



            bool isTruce = _eGM.TruceEnt_ActivatedDictCom.IsActivated(true)
                && _eGM.TruceEnt_ActivatedDictCom.IsActivated(false);


            if (isTruce)
            {
                _eGM.MotionEnt_AmountCom.Amount += Random.Range(4500, 5500);
                _photonPunRPC.TruceToGeneral(RpcTarget.All, true, false, _eGM.MotionEnt_AmountCom.Amount);

                int random;

                for (int x = 0; x < _eGM.Xamount; x++)
                    for (int y = 0; y < _eGM.Yamount; y++)
                    {
                        if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveUnit)
                        {
                            if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                            {
                                switch (_eGM.CellUnitEnt_UnitTypeCom(x, y).UnitType)
                                {
                                    case UnitTypes.None:
                                        break;

                                    case UnitTypes.King:
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddAmountUnits(UnitTypes.King, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                        _eGM.UnitInfoEnt_UnitInventorCom.SetSettedKing(_eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient, false);
                                        break;

                                    case UnitTypes.Pawn:
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddAmountUnits(UnitTypes.Pawn, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                        break;

                                    case UnitTypes.PawnSword:
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddAmountUnits(UnitTypes.PawnSword, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                        break;

                                    case UnitTypes.Rook:
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddAmountUnits(UnitTypes.Rook, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                        break;

                                    case UnitTypes.RookCrossbow:
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddAmountUnits(UnitTypes.RookCrossbow, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                        break;

                                    case UnitTypes.Bishop:
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddAmountUnits(UnitTypes.Bishop, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                        break;

                                    case UnitTypes.BishopCrossbow:
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddAmountUnits(UnitTypes.BishopCrossbow, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                        break;

                                    default:
                                        break;
                                }

                                CellUnitWorker.ResetUnit(x, y);
                            }
                        }

                        if (_eGM.CellBuildEnt_BuilTypeCom(x, y).HaveBuilding)
                        {
                            if (_eGM.CellBuildEnt_OwnerCom(x, y).HaveOwner)
                            {
                                switch (_eGM.CellBuildEnt_BuilTypeCom(x, y).BuildingType)
                                {
                                    case BuildingTypes.None:
                                        break;

                                    case BuildingTypes.City:
                                        CellBuildingWorker.ResetBuilding(true, x, y);
                                        break;

                                    case BuildingTypes.Farm:
                                        //_eGM.FoodEnt_AmountDictCom.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                                        //_eGM.WoodEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                                        //_eGM.OreEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.ORE_FOR_BUILDING_FARM;
                                        //_eGM.IronEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.IRON_FOR_BUILDING_FARM;
                                        //_eGM.GoldEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                                        CellBuildingWorker.ResetBuilding(true, x, y);
                                        break;

                                    case BuildingTypes.Woodcutter:
                                        //_eGM.FoodEnt_AmountDictCom.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                                        //_eGM.WoodEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                                        //_eGM.OreEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                                        //_eGM.IronEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                                        //_eGM.GoldEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;
                                        CellBuildingWorker.ResetBuilding(true, x, y);
                                        break;

                                    case BuildingTypes.Mine:
                                        //_eGM.FoodEnt_AmountDictCom.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                                        //_eGM.WoodEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                                        //_eGM.OreEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.ORE_FOR_BUILDING_MINE;
                                        //_eGM.IronEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.IRON_FOR_BUILDING_MINE;
                                        //_eGM.GoldEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.GOLD_FOR_BUILDING_MINE;
                                        CellBuildingWorker.ResetBuilding(true, x, y);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }

                        if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizer
                            && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveMountain && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultTree
                                 && _eGM.CellBuildEnt_BuilTypeCom(x, y).BuildingType != BuildingTypes.City)
                        {
                            random = Random.Range(0, 100);

                            if (random <= 5)
                            {
                                _eGM.CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.Fertilizer);
                            }
                        }

                        if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveYoungTree)
                        {
                            _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.YoungForest);
                            _eGM.CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.AdultForest);
                        }

                        if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizer && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveMountain
                             && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultTree && _eGM.CellBuildEnt_BuilTypeCom(x, y).BuildingType != BuildingTypes.City)
                        {
                            random = Random.Range(0, 100);

                            if (random <= 5)
                            {
                                _eGM.CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.AdultForest);
                            }
                        }
                    }


                _eGM.TruceEnt_ActivatedDictCom.SetIsActivated(true, false);
                _eGM.TruceEnt_ActivatedDictCom.SetIsActivated(false, false);

                _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Food, true, 10);
                _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Food, false, 10);

                _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, true, 10);
                _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, false, 10);

                _photonPunRPC.DoneToGeneral(RpcTarget.All, false, false, _eGM.MotionEnt_AmountCom.Amount);
            }
        }

        
    }
}
