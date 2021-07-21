using Assets.Scripts;
using Assets.Scripts.Static.Cell;
using Photon.Pun;
using UnityEngine;
using static Assets.Scripts.CellEnvironmentWorker;
using static Assets.Scripts.CellUnitWorker;

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
                var xy = new int[] { x, y };

                CellEffectsWorker.ResetEffect(EffectTypes.Fire, xy);

                if (HaveAnyUnit(xy))
                {
                    if (HaveOwner(xy))
                    {
                        switch (UnitType(xy))
                        {
                            case UnitTypes.None:
                                break;

                            case UnitTypes.King:
                                _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.King, IsMasterClient(xy));
                                _eGM.UnitInfoEnt_UnitInventorCom.SetSettedKing(IsMasterClient(xy), false);
                                break;

                            case UnitTypes.Pawn:
                                _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.Pawn, IsMasterClient(xy));
                                break;

                            case UnitTypes.PawnSword:
                                _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.PawnSword, IsMasterClient(xy));
                                break;

                            case UnitTypes.Rook:
                                _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.Rook, IsMasterClient(xy));
                                break;

                            case UnitTypes.RookCrossbow:
                                _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.RookCrossbow, IsMasterClient(xy));
                                break;

                            case UnitTypes.Bishop:
                                _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.Bishop, IsMasterClient(xy));
                                break;

                            case UnitTypes.BishopCrossbow:
                                _eGM.UnitInfoEnt_UnitInventorCom.AddUnitsInInventor(UnitTypes.BishopCrossbow, IsMasterClient(xy));
                                break;

                            default:
                                break;
                        }

                        if (HaveOwner(xy))
                        {
                            ResetPlayerUnit(xy);
                        }
                        else
                        {
                            ResetBotUnit(x, y);
                        }
                    }
                }

                if (HaveEnvironment(EnvironmentTypes.YoungForest, xy))
                {
                    ResetEnvironment(EnvironmentTypes.YoungForest, xy);
                    SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
                }

                if (!HaveEnvironment(EnvironmentTypes.Fertilizer, xy)
                    && !HaveEnvironment(EnvironmentTypes.Mountain, xy) && !HaveEnvironment(EnvironmentTypes.AdultForest, xy)
                         && _eGM.CellBuildEnt_BuilTypeCom(x, y).BuildingType != BuildingTypes.City)
                {
                    random = Random.Range(0, 100);

                    if (random <= 20)
                    {
                        SetNewEnvironment(EnvironmentTypes.Fertilizer, xy);
                    }
                }

                if (!HaveEnvironment(EnvironmentTypes.Fertilizer, xy) && !HaveEnvironment(EnvironmentTypes.Mountain, xy)
                     && !HaveEnvironment(EnvironmentTypes.AdultForest, xy) && _eGM.CellBuildEnt_BuilTypeCom(x, y).BuildingType != BuildingTypes.City)
                {
                    random = Random.Range(0, 100);

                    if (random <= 5)
                    {
                        SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
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