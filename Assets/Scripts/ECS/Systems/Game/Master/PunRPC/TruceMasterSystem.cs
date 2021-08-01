using Assets.Scripts;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Game.UI;
using Assets.Scripts.Workers.Info;
using Photon.Pun;
using UnityEngine;

internal sealed class TruceMasterSystem : SystemMasterReduction
{
    public override void Run()
    {
        base.Run();

        int random;

        for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

                CellFireDataWorker.ResetFire(xy);
                CellFireDataWorker.ResetTimeSteps(xy);

                if (CellUnitsDataWorker.HaveAnyUnit(xy))
                {
                    var unitType = CellUnitsDataWorker.UnitType(xy);

                    if (CellUnitsDataWorker.HaveOwner(xy))
                    {
                        var isMasterKey = CellUnitsDataWorker.IsMasterClient(xy);

                        InfoUnitsContainer.AddUnitsInInventor(unitType, isMasterKey);

                        InfoUnitsContainer.RemoveUnitInCondition(CellUnitsDataWorker.ConditionType(xy), unitType, isMasterKey, xy);
                        InfoUnitsContainer.RemoveAmountUnitsInGame(unitType, isMasterKey, xy);

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


        if (InfoUnitsContainer.GetAmountUnitsInGame(UnitTypes.Pawn, true) <= 0
            && InfoUnitsContainer.GetAmountUnitsInGame(UnitTypes.Pawn, true) <= 0)
        {
            InfoUnitsContainer.AddUnitsInInventor(UnitTypes.Pawn, true);
        }

        if (InfoUnitsContainer.GetAmountUnitsInGame(UnitTypes.Pawn, false) <= 0
            && InfoUnitsContainer.GetAmountUnitsInGame(UnitTypes.Pawn, false) <= 0)
        {
            InfoUnitsContainer.AddUnitsInInventor(UnitTypes.Pawn, false);
        }

        PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, Main.Instance.ECSmanager.EntGameGeneralUIViewManager.MotionEnt_AmountCom.AmountMotions);
        PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);

        DownDonerUIWorker.SetDoned(true, default);
        DownDonerUIWorker.SetDoned(false, default);
    }
}