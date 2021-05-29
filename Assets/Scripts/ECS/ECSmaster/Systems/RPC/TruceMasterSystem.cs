using Photon.Pun;
using UnityEngine;
using static MainGame;

internal sealed class TruceMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo Info => _eGM.RpcGeneralEnt_FromInfoCom.FromInfo;

    internal TruceMasterSystem(ECSmanager eCSmanager) : base(eCSmanager) { }

    public override void Run()
    {
        base.Run();
      
        _photonPunRPC.TruceToGeneral(Info.Sender, false, _eGM.RpcGeneralEnt_FromInfoCom.IsActived, _eGM.UpdatorEntityAmountComponent.Amount);

        _eGM.TruceEnt_ActivatedDictCom.IsActivatedDictionary[Info.Sender.IsMasterClient] = _eGM.RpcGeneralEnt_FromInfoCom.IsActived;


        bool isTruce = Instance.StartValuesGameConfig.IS_TEST ||
        _eGM.TruceEnt_ActivatedDictCom.IsActivatedDictionary[true]
            && _eGM.TruceEnt_ActivatedDictCom.IsActivatedDictionary[false];



        if (isTruce)
        {
            _eGM.UpdatorEntityAmountComponent.Amount += Random.Range(4500, 5500);
            _photonPunRPC.TruceToGeneral(RpcTarget.All, true, false, _eGM.UpdatorEntityAmountComponent.Amount);

            int random;

            for (int x = 0; x < _eGM.Xamount; x++)
            {
                for (int y = 0; y < _eGM.Yamount; y++)
                {
                    if (_eGM.CellEnt_CellUnitCom(x, y).HaveUnit)
                    {
                        switch (_eGM.CellEnt_CellUnitCom(x, y).UnitType)
                        {
                            case UnitTypes.None:
                                break;

                            case UnitTypes.King:
                                _eGM.InfoEnt_UnitsInfoCom.AmountKingDict[_eGM.CellEnt_CellUnitCom(x, y).IsMasterClient] += 1;
                                _eGM.InfoEnt_UnitsInfoCom.IsSettedKingDict[_eGM.CellEnt_CellUnitCom(x, y).IsMasterClient] = false;
                                break;

                            case UnitTypes.Pawn:
                                _eGM.InfoEnt_UnitsInfoCom.AmountPawnDict[_eGM.CellEnt_CellUnitCom(x, y).IsMasterClient] += 1;
                                break;

                            case UnitTypes.Rook:
                                _eGM.InfoEnt_UnitsInfoCom.AmountRookDict[_eGM.CellEnt_CellUnitCom(x, y).IsMasterClient] += 1;
                                break;

                            case UnitTypes.Bishop:
                                _eGM.InfoEnt_UnitsInfoCom.AmountBishopDict[_eGM.CellEnt_CellUnitCom(x, y).IsMasterClient] += 1;
                                break;

                            default:
                                break;
                        }

                        _eGM.CellEnt_CellUnitCom(x, y).ResetUnit();

                    }

                    if (_eGM.CellBuildingEnt_BuildingTypeCom(x, y).HaveBuilding)
                    {
                        switch (_eGM.CellBuildingEnt_BuildingTypeCom(x, y).BuildingType)
                        {
                            case BuildingTypes.None:
                                break;

                            case BuildingTypes.City:
                                _cellWorker.ResetBuilding(x, y);
                                break;

                            case BuildingTypes.Farm:
                                //_eGM.FoodEnt_AmountDictCom.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.FOOD_FOR_BUILDING_FARM;
                                //_eGM.WoodEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.WOOD_FOR_BUILDING_FARM;
                                //_eGM.OreEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.ORE_FOR_BUILDING_FARM;
                                //_eGM.IronEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.IRON_FOR_BUILDING_FARM;
                                //_eGM.GoldEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.GOLD_FOR_BUILDING_FARM;
                                _cellWorker.ResetBuilding(x, y);
                                break;

                            case BuildingTypes.Woodcutter:
                                //_eGM.FoodEnt_AmountDictCom.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.FOOD_FOR_BUILDING_WOODCUTTER;
                                //_eGM.WoodEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.WOOD_FOR_BUILDING_WOODCUTTER;
                                //_eGM.OreEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.ORE_FOR_BUILDING_WOODCUTTER;
                                //_eGM.IronEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.IRON_FOR_BUILDING_WOODCUTTER;
                                //_eGM.GoldEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.GOLD_FOR_BUILDING_WOODCUTTER;
                                _cellWorker.ResetBuilding(x, y);
                                break;

                            case BuildingTypes.Mine:
                                //_eGM.FoodEnt_AmountDictCom.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.FOOD_FOR_BUILDING_MINE;
                                //_eGM.WoodEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.WOOD_FOR_BUILDING_MINE;
                                //_eGM.OreEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.ORE_FOR_BUILDING_MINE;
                                //_eGM.IronEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.IRON_FOR_BUILDING_MINE;
                                //_eGM.GoldEAmountDictC.AmountDict[_eGM.CellEnt_CellBuildingCom(x, y).IsMasterOwner] += _startValuesGameConfig.GOLD_FOR_BUILDING_MINE;
                                _cellWorker.ResetBuilding(x, y);
                                break;

                            default:
                                break;
                        }
                    }

                    if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizer
                        && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveMountain && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultTree
                             && _eGM.CellBuildingEnt_BuildingTypeCom(x, y).BuildingType != BuildingTypes.City)
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
                         && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultTree && _eGM.CellBuildingEnt_BuildingTypeCom(x,y).BuildingType != BuildingTypes.City)
                    {
                        random = Random.Range(0, 100);

                        if (random <= 5)
                        {
                            _eGM.CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.AdultForest);
                        }
                    }
                }
            }


            //for (int x = 0; x < _eGM.Xamount; x++)
            //{
            //    for (int y = 0; y < _eGM.Yamount; y++)
            //    {
            //        if(!_eGM.CellEnt_CellEnvCom(x,y).HaveMountain && !_eGM.CellEnt_CellEnvCom(x, y).HaveAdultTree 
            //            && _eGM.CellEnt_CellBuildingCom(x, y).BuildingType != BuildingTypes.City)
            //        {
            //            random = Random.Range(0, 100);

            //            if (random <= Instance.StartValuesGameConfig.PERCENT_FOOD)
            //            {
            //                _eGM.CellEnt_CellEnvCom(x, y).SetResetEnvironment(true, EnvironmentTypes.Fertilizer);
            //            }
            //        }
            //    }
            //}


            _eGM.TruceEnt_ActivatedDictCom.IsActivatedDictionary[true] = false;
            _eGM.TruceEnt_ActivatedDictCom.IsActivatedDictionary[false] = false;
        }
    }
}
