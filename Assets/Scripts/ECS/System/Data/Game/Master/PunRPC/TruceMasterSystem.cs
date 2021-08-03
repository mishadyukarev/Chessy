using Assets.Scripts;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class TruceMasterSystem : SystemMasterReduction
{
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter;

    public override void Run()
    {
        base.Run();

        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

        int random;

        for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

                CellFireDataSystem.HaveFireCom(xy).Disable();

                if (CellUnitsDataSystem.HaveAnyUnit(xy))
                {
                    var unitType = CellUnitsDataSystem.UnitType(xy);

                    if (CellUnitsDataSystem.HaveOwner(xy))
                    {
                        var isMasterKey = CellUnitsDataSystem.IsMasterClient(xy);

                        InitSystem.UnitInventorCom.AddUnitsInInventor(unitType, isMasterKey);

                        InfoUnitsDataContainer.RemoveUnitInCondition(CellUnitsDataSystem.ConditionType(xy), unitType, isMasterKey, xy);
                        xyUnitsCom.RemoveAmountUnitsInGame(unitType, isMasterKey, xy);

                        CellUnitsDataSystem.ResetUnit(xy);
                    }
                }


                if (CellBuildDataSystem.BuildTypeCom(xy).HaveBuild)
                {

                }

                else
                {
                    if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.YoungForest, xy))
                    {
                        CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.YoungForest, xy);
                        CellEnvrDataSystem.SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
                    }

                    if (!CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Fertilizer, xy)
                        && !CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.Mountain, xy)
                        && !CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                    {
                        random = Random.Range(0, 100);

                        if (random <= 20)
                        {
                            CellEnvrDataSystem.SetNewEnvironment(EnvironmentTypes.Fertilizer, xy);
                        }
                        else
                        {
                            random = Random.Range(0, 100);

                            if (random <= 5)
                            {
                                CellEnvrDataSystem.SetNewEnvironment(EnvironmentTypes.AdultForest, xy);
                            }
                        }
                    }
                }
            }

        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Food, true, 0);
        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Food, false, 0);

        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Wood, true, 0);
        //InfoResourcesWorker.AddAmountResources(ResourceTypes.Wood, false, 0);


        if (xyUnitsCom.GetAmountUnitsInGame(UnitTypes.Pawn, true) <= 0
            && xyUnitsCom.GetAmountUnitsInGame(UnitTypes.Pawn, true) <= 0)
        {
            InitSystem.UnitInventorCom.AddUnitsInInventor(UnitTypes.Pawn, true);
        }

        if (xyUnitsCom.GetAmountUnitsInGame(UnitTypes.Pawn, false) <= 0
            && xyUnitsCom.GetAmountUnitsInGame(UnitTypes.Pawn, false) <= 0)
        {
            InitSystem.UnitInventorCom.AddUnitsInInventor(UnitTypes.Pawn, false);
        }

        PhotonPunRPC.SetAmountMotionToOther(RpcTarget.All, Main.Instance.ECSmanager.EntViewGameGeneralUIManager.MotionEnt_AmountCom.AmountMotions);
        PhotonPunRPC.ActiveAmountMotionUIToGeneral(RpcTarget.All);

        DownDonerUIDataContainer.SetDoned(true, default);
        DownDonerUIDataContainer.SetDoned(false, default);
    }
}