using Assets.Scripts;
using Photon.Pun;
using UnityEngine;

internal sealed class TruceMasterSystem : RPCMasterSystemReduction
{
    private PhotonMessageInfo Info => _eGM.RpcGeneralEnt_RPCCom.FromInfo;

    public override void Run()
    {
        base.Run();

        _eGM.MotionEnt_AmountCom.AddAmount(Random.Range(4500, 5500));

        int random;

        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                _eGM.CellEffectEnt_CellEffectCom(x, y).ResetEffect(EffectTypes.Fire);


                if (_eGM.CellUnitEnt_UnitTypeCom(x, y).HaveAnyUnit)
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

                        if (_eGM.CellUnitEnt_CellOwnerCom(x, y).HaveOwner)
                        {
                            CellUnitWorker.ResetPlayerUnit(x, y);
                        }
                        else
                        {
                            CellUnitWorker.ResetBotUnit(x, y);
                        }
                    }
                }

                //if (_eGM.CellBuildEnt_BuilTypeCom(x, y).HaveBuilding)
                //{
                //    if (_eGM.CellBuildEnt_OwnerCom(x, y).HaveOwner)
                //    {
                //        switch (_eGM.CellBuildEnt_BuilTypeCom(x, y).BuildingType)
                //        {
                //            case BuildingTypes.None:
                //                break;

                //            case BuildingTypes.City:
                //                //CellBuildingWorker.ResetBuilding(true, x, y);
                //                break;

                //            case BuildingTypes.Farm:
                //                CellBuildingWorker.ResetBuilding(true, x, y);
                //                break;

                //            case BuildingTypes.Woodcutter:
                //                CellBuildingWorker.ResetBuilding(true, x, y);
                //                break;

                //            case BuildingTypes.Mine:
                //                CellBuildingWorker.ResetBuilding(true, x, y);
                //                break;

                //            default:
                //                break;
                //        }
                //    }
                //}
                if (_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.YoungForest))
                {
                    _eGM.CellEnvEnt_CellEnvCom(x, y).ResetEnvironment(EnvironmentTypes.YoungForest);
                    _eGM.CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.AdultForest);
                }

                if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.Fertilizer)
                    && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.Mountain) && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.AdultForest)
                         && _eGM.CellBuildEnt_BuilTypeCom(x, y).BuildingType != BuildingTypes.City)
                {
                    random = Random.Range(0, 100);

                    if (random <= 20)
                    {
                        _eGM.CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.Fertilizer);
                    }
                }

                if (!_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.Fertilizer) && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.Mountain)
                     && !_eGM.CellEnvEnt_CellEnvCom(x, y).HaveEnvironment(EnvironmentTypes.AdultForest) && _eGM.CellBuildEnt_BuilTypeCom(x, y).BuildingType != BuildingTypes.City)
                {
                    random = Random.Range(0, 100);

                    if (random <= 5)
                    {
                        _eGM.CellEnvEnt_CellEnvCom(x, y).SetNewEnvironment(EnvironmentTypes.AdultForest);
                    }
                }
            }

        _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Food, true, 15);
        _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Food, false, 15);

        _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, true, 15);
        _eGM.EconomyEnt_EconomyCom.AddAmountResources(ResourceTypes.Wood, false, 15);

        //_photonPunRPC.DoneToGeneral(RpcTarget.All, false);
        _photonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGM.MotionEnt_AmountCom.Amount);
        _photonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);
        _eGM.DonerUIEnt_IsActivatedDictCom.ResetAll();
        //}
    }
}