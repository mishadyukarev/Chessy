using Assets.Scripts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Info;
using Photon.Pun;
using UnityEngine;

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

                CellFireDataWorker.ResetFire(xy);
                CellFireDataWorker.ResetTimeSteps(xy);

                if (CellUnitsDataWorker.HaveAnyUnit(xy))
                {
                    if (CellUnitsDataWorker.HaveOwner(xy))
                    {
                        switch (CellUnitsDataWorker.UnitType(xy))
                        {
                            case UnitTypes.None:
                                break;

                            case UnitTypes.King:
                                InventorUnitsDataWorker.AddUnitsInInventor(UnitTypes.King, CellUnitsDataWorker.IsMasterClient(xy));
                                InfoUnitsWorker.TakeAmountUnitInGame(UnitTypes.King, CellUnitsDataWorker.IsMasterClient(xy), xy);
                                break;

                            case UnitTypes.Pawn:
                                InventorUnitsDataWorker.AddUnitsInInventor(UnitTypes.Pawn, CellUnitsDataWorker.IsMasterClient(xy));
                                break;

                            case UnitTypes.PawnSword:
                                InventorUnitsDataWorker.AddUnitsInInventor(UnitTypes.PawnSword, CellUnitsDataWorker.IsMasterClient(xy));
                                break;

                            case UnitTypes.Rook:
                                InventorUnitsDataWorker.AddUnitsInInventor(UnitTypes.Rook, CellUnitsDataWorker.IsMasterClient(xy));
                                break;

                            case UnitTypes.RookCrossbow:
                                InventorUnitsDataWorker.AddUnitsInInventor(UnitTypes.RookCrossbow, CellUnitsDataWorker.IsMasterClient(xy));
                                break;

                            case UnitTypes.Bishop:
                                InventorUnitsDataWorker.AddUnitsInInventor(UnitTypes.Bishop, CellUnitsDataWorker.IsMasterClient(xy));
                                break;

                            case UnitTypes.BishopCrossbow:
                                InventorUnitsDataWorker.AddUnitsInInventor(UnitTypes.BishopCrossbow, CellUnitsDataWorker.IsMasterClient(xy));
                                break;

                            default:
                                break;
                        }

                        CellUnitsDataWorker.ResetUnit(xy);
                    }
                }


                if (CellBuildingsDataWorker.HaveAnyBuilding(xy))
                {

                }

                else
                {
                    if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.YoungForest, xy))
                    {
                        CellEnvirDataWorker.ResetEnvironment(EnvironmentTypes.YoungForest, xy);
                        CellEnvirDataWorker.SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
                    }

                    if (!CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Fertilizer, xy)
                        && !CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.Mountain, xy)
                        && !CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                    {
                        random = Random.Range(0, 100);

                        if (random <= 20)
                        {
                            CellEnvirDataWorker.SetNewEnvironment(EnvironmentTypes.Fertilizer, xy);
                        }
                        else
                        {
                            random = Random.Range(0, 100);

                            if (random <= 5)
                            {
                                CellEnvirDataWorker.SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
                            }
                        }
                    }
                }
            }

        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Food, true, 0);
        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Food, false, 0);

        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Wood, true, 0);
        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Wood, false, 0);


        if (InfoUnitsWorker.AmountUnitsInGame(UnitTypes.Pawn, true) <= 0
            && InfoUnitsWorker.AmountUnitsInGame(UnitTypes.Pawn, true) <= 0)
        {
            InventorUnitsDataWorker.AddUnitsInInventor(UnitTypes.Pawn, true);
        }

        if (InfoUnitsWorker.AmountUnitsInGame(UnitTypes.Pawn, false) <= 0
            && InfoUnitsWorker.AmountUnitsInGame(UnitTypes.Pawn, false) <= 0)
        {
            InventorUnitsDataWorker.AddUnitsInInventor(UnitTypes.Pawn, false);
        }

        PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, _eGGUIM.MotionEnt_AmountCom.Amount);
        PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);
        _eGGUIM.DonerUIEnt_IsActivatedDictCom.ResetAll();
    }
}