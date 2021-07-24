using Assets.Scripts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Info;
using Photon.Pun;
using UnityEngine;
using static Assets.Scripts.CellEnvironmentWorker;
using static Assets.Scripts.CellUnitWorker;

internal sealed class TruceMasterSystem : RPCMasterSystemReduction
{
    public override void Run()
    {
        base.Run();

        _eGGUIM.MotionEnt_AmountCom.Amount += Random.Range(4500, 5500);

        int random;

        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                var xy = new int[] { x, y };

                CellFireWorker.ResetEffect(EffectTypes.Fire, xy);

                if (HaveAnyUnit(xy))
                {
                    if (HaveOwner(xy))
                    {
                        switch (UnitType(xy))
                        {
                            case UnitTypes.None:
                                break;

                            case UnitTypes.King:
                                InfoUnitsWorker.AddUnitsInInventor(UnitTypes.King, IsMasterClient(xy));
                                InfoUnitsWorker.SetSettedKing(IsMasterClient(xy), false);
                                break;

                            case UnitTypes.Pawn:
                                InfoUnitsWorker.AddUnitsInInventor(UnitTypes.Pawn, IsMasterClient(xy));
                                break;

                            case UnitTypes.PawnSword:
                                InfoUnitsWorker.AddUnitsInInventor(UnitTypes.PawnSword, IsMasterClient(xy));
                                break;

                            case UnitTypes.Rook:
                                InfoUnitsWorker.AddUnitsInInventor(UnitTypes.Rook, IsMasterClient(xy));
                                break;

                            case UnitTypes.RookCrossbow:
                                InfoUnitsWorker.AddUnitsInInventor(UnitTypes.RookCrossbow, IsMasterClient(xy));
                                break;

                            case UnitTypes.Bishop:
                                InfoUnitsWorker.AddUnitsInInventor(UnitTypes.Bishop, IsMasterClient(xy));
                                break;

                            case UnitTypes.BishopCrossbow:
                                InfoUnitsWorker.AddUnitsInInventor(UnitTypes.BishopCrossbow, IsMasterClient(xy));
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
                         && !CellBuildingWorker.IsBuildingType(BuildingTypes.City, xy))
                {
                    random = Random.Range(0, 100);

                    if (random <= 20)
                    {
                        SetNewEnvironment(EnvironmentTypes.Fertilizer, xy);
                    }
                }

                if (!HaveEnvironment(EnvironmentTypes.Fertilizer, xy) && !HaveEnvironment(EnvironmentTypes.Mountain, xy)
                     && !HaveEnvironment(EnvironmentTypes.AdultForest, xy) && CellBuildingWorker.IsBuildingType(BuildingTypes.City, xy))
                {
                    random = Random.Range(0, 100);

                    if (random <= 5)
                    {
                        SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
                    }
                }
            }

        InfoResourcesWorker.AddAmountResources(ResourceTypes.Food, true, 15);
        InfoResourcesWorker.AddAmountResources(ResourceTypes.Food, false, 15);

        InfoResourcesWorker.AddAmountResources(ResourceTypes.Wood, true, 15);
        InfoResourcesWorker.AddAmountResources(ResourceTypes.Wood, false, 15);

        //PhotonPunRPC.DoneToGeneral(RpcTarget.All, false);
        PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.Amount);
        PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);
        _eGGUIM.DonerUIEnt_IsActivatedDictCom.ResetAll();
        //}
    }
}