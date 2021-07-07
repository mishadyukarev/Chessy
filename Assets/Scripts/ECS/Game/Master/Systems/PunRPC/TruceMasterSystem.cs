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
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.King, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                        _eGM.UnitInfoEnt_UnitInventorCom.SetSettedKing(_eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient, false);
                                        break;

                                    case UnitTypes.Pawn:
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.Pawn, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                        break;

                                    case UnitTypes.PawnSword:
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.PawnSword, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                        break;

                                    case UnitTypes.Rook:
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.Rook, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                        break;

                                    case UnitTypes.RookCrossbow:
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.RookCrossbow, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                        break;

                                    case UnitTypes.Bishop:
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.Bishop, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
                                        break;

                                    case UnitTypes.BishopCrossbow:
                                        _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.BishopCrossbow, _eGM.CellUnitEnt_CellOwnerCom(x, y).IsMasterClient);
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
                                        //CellBuildingWorker.ResetBuilding(true, x, y);
                                        break;

                                    case BuildingTypes.Farm:
                                        CellBuildingWorker.ResetBuilding(true, x, y);
                                        break;

                                    case BuildingTypes.Woodcutter:
                                        CellBuildingWorker.ResetBuilding(true, x, y);
                                        break;

                                    case BuildingTypes.Mine:
                                        CellBuildingWorker.ResetBuilding(true, x, y);
                                        break;

                                    default:
                                        break;
                                }
                            }
                        }

                        if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveFertilizer
                            && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveMountain && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultForest
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
                             && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveAdultForest && _eGM.CellBuildEnt_BuilTypeCom(x, y).BuildingType != BuildingTypes.City)
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
