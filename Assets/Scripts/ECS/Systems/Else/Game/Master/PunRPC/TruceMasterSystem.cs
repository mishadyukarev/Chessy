using Assets.Scripts;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Component.Data.UI.Game.General;
using Assets.Scripts.ECS.Component.View.UI.Game.General;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.Workers.Game.UI;
using Leopotam.Ecs;
using Photon.Pun;
using UnityEngine;

internal sealed class TruceMasterSystem : IEcsRunSystem
{
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter = default;
    private EcsFilter<InventorUnitsComponent> _inventorUnitsFilter = default;
    private EcsFilter<MotionsDataUIComponent> _motionsUIFilter = default;
    private EcsFilter<DonerDataUIComponent, DonerViewUIComponent> _donerUIFilter = default;

    public void Run()
    {
        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);
        ref var inventorUnitsCom = ref _inventorUnitsFilter.Get1(0);

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

                        inventorUnitsCom.AddUnitsInInventor(unitType, isMasterKey);

                        MainGameSystem.XyUnitsContitionCom.RemoveUnitInCondition(CellUnitsDataSystem.ConditionType(xy), unitType, isMasterKey, xy);
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


        if (xyUnitsCom.GetAmountUnitsInGame(UnitTypes.Pawn, true) <= 0
            && xyUnitsCom.GetAmountUnitsInGame(UnitTypes.Pawn, true) <= 0)
        {
            inventorUnitsCom.AddUnitsInInventor(UnitTypes.Pawn, true);
        }

        if (xyUnitsCom.GetAmountUnitsInGame(UnitTypes.Pawn, false) <= 0
            && xyUnitsCom.GetAmountUnitsInGame(UnitTypes.Pawn, false) <= 0)
        {
            inventorUnitsCom.AddUnitsInInventor(UnitTypes.Pawn, false);
        }

        RPCGameSystem.SetAmountMotionToOther(RpcTarget.All, _motionsUIFilter.Get1(0).AmountMotions);
        RPCGameSystem.ActiveAmountMotionUIToGeneral(RpcTarget.All);

        _donerUIFilter.Get1(0).SetDoned(true, default);
        _donerUIFilter.Get1(0).SetDoned(false, default);
    }
}