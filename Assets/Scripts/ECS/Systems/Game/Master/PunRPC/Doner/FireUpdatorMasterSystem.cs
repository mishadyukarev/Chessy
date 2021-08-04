using Assets.Scripts;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.Component;
using Assets.Scripts.ECS.Game.General.Systems.StartFill;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Assets.Scripts.ECS.System.View.Game.General.Cell;
using Assets.Scripts.Workers.Cell;
using Leopotam.Ecs;

internal sealed class FireUpdatorMasterSystem : SystemMasterReduction
{
    private EcsFilter<XyUnitsComponent> _xyUnitsFilter;

    public override void Run()
    {
        base.Run();

        ref var xyUnitsCom = ref _xyUnitsFilter.Get1(0);

        for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

                if (CellFireDataSystem.HaveFireCom(xy).HaveFire)
                {
                    CellEnvrDataSystem.TakeAmountResources(EnvironmentTypes.AdultForest, xy, 2);

                    if (CellUnitsDataSystem.HaveAnyUnit(xy))
                    {
                        CellUnitsDataSystem.TakeAmountHealth(xy, 40);

                        if (!CellUnitsDataSystem.HaveAmountHealth(xy))
                        {
                            var conditionType = CellUnitsDataSystem.ConditionType(xy);
                            var unitType = CellUnitsDataSystem.UnitType(xy);
                            var key = CellUnitsDataSystem.IsMasterClient(xy);

                            xyUnitsCom.RemoveAmountUnitsInGame(unitType, key, xy);
                            InitSystem.XyUnitsContitionCom.RemoveUnitInCondition(conditionType, unitType, key, xy);
                            CellUnitsDataSystem.ResetUnit(xy);
                        }
                    }



                    if (!CellEnvrDataSystem.HaveResources(EnvironmentTypes.AdultForest, xy))
                    {
                        if (CellBuildDataSystem.BuildTypeCom(xy).HaveBuild)
                        {
                            var buildType = CellBuildDataSystem.BuildTypeCom(xy).BuildingType;
                            var key = CellBuildDataSystem.OwnerCom(xy).IsMasterClient;

                            InitSystem.XyBuildingsCom.RemoveXyBuild(buildType, key, xy);
                            CellBuildDataSystem.ResetBuild(xy);
                        }

                        CellEnvrDataSystem.ResetEnvironment(EnvironmentTypes.AdultForest, xy);

                        CellFireDataSystem.HaveFireCom(xy).Disable();


                        var aroundXYList = CellSpaceWorker.TryGetXyAround(xy);
                        foreach (var xy1 in aroundXYList)
                        {
                            if (CellViewSystem.IsActiveSelfParentCell(xy1))
                            {
                                if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy1))
                                {
                                    CellFireDataSystem.HaveFireCom(xy1).Disable();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
