using Assets.Scripts;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Game.UI;
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

                CellFireDataContainer.ResetFire(xy);

                if (CellUnitsDataContainer.HaveAnyUnit(xy))
                {
                    var unitType = CellUnitsDataContainer.UnitType(xy);

                    if (CellUnitsDataContainer.HaveOwner(xy))
                    {
                        var isMasterKey = CellUnitsDataContainer.IsMasterClient(xy);

                        InfoUnitsDataContainer.AddUnitsInInventor(unitType, isMasterKey);

                        InfoUnitsDataContainer.RemoveUnitInCondition(CellUnitsDataContainer.ConditionType(xy), unitType, isMasterKey, xy);
                        InfoUnitsDataContainer.RemoveAmountUnitsInGame(unitType, isMasterKey, xy);

                        CellUnitsDataContainer.ResetUnit(xy);
                    }
                }


                if (CellBuildDataContainer.HaveAnyBuilding(xy))
                {

                }

                else
                {
                    if (CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.YoungForest, xy))
                    {
                        CellEnvirDataContainer.ResetEnvironment(EnvironmentTypes.YoungForest, xy);
                        CellEnvirDataContainer.SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
                    }

                    if (!CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.Fertilizer, xy)
                        && !CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.Mountain, xy)
                        && !CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                    {
                        random = Random.Range(0, 100);

                        if (random <= 20)
                        {
                            CellEnvirDataContainer.SetNewEnvironment(EnvironmentTypes.Fertilizer, xy);
                        }
                        else
                        {
                            random = Random.Range(0, 100);

                            if (random <= 5)
                            {
                                CellEnvirDataContainer.SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
                            }
                        }
                    }
                }
            }

        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Food, true, 0);
        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Food, false, 0);

        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Wood, true, 0);
        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Wood, false, 0);


        if (InfoUnitsDataContainer.GetAmountUnitsInGame(UnitTypes.Pawn, true) <= 0
            && InfoUnitsDataContainer.GetAmountUnitsInGame(UnitTypes.Pawn, true) <= 0)
        {
            InfoUnitsDataContainer.AddUnitsInInventor(UnitTypes.Pawn, true);
        }

        if (InfoUnitsDataContainer.GetAmountUnitsInGame(UnitTypes.Pawn, false) <= 0
            && InfoUnitsDataContainer.GetAmountUnitsInGame(UnitTypes.Pawn, false) <= 0)
        {
            InfoUnitsDataContainer.AddUnitsInInventor(UnitTypes.Pawn, false);
        }

        PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, Main.Instance.ECSmanager.EntGameGeneralUIViewManager.MotionEnt_AmountCom.AmountMotions);
        PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);

        DownDonerUIDataContainer.SetDoned(true, default);
        DownDonerUIDataContainer.SetDoned(false, default);
    }
}