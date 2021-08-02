using Assets.Scripts;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Game.Else.Info.Units;

internal sealed class FireUpdatorMasterSystem : SystemMasterReduction
{

    public override void Run()
    {
        base.Run();

        for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
        {
            for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

                if (CellFireDataContainer.HaveFire(xy))
                {
                    CellEnvirDataContainer.TakeAmountResources(EnvironmentTypes.AdultForest, xy, 2);

                    if (CellUnitsDataContainer.HaveAnyUnit(xy))
                    {
                        CellUnitsDataContainer.TakeAmountHealth(xy, 40);

                        if (!CellUnitsDataContainer.HaveAmountHealth(xy))
                        {
                            var conditionType = CellUnitsDataContainer.ConditionType(xy);
                            var unitType = CellUnitsDataContainer.UnitType(xy);
                            var key = CellUnitsDataContainer.IsMasterClient(xy);

                            InfoUnitsDataContainer.RemoveAmountUnitsInGame(unitType, key, xy);
                            InfoUnitsDataContainer.RemoveUnitInCondition(conditionType, unitType, key, xy);
                            CellUnitsDataContainer.ResetUnit(xy);
                        }
                    }



                    if (!CellEnvirDataContainer.HaveResources(EnvironmentTypes.AdultForest, xy))
                    {
                        if (CellBuildDataContainer.HaveAnyBuilding(xy))
                        {
                            var buildType = CellBuildDataContainer.GetBuildingType(xy);
                            var key = CellBuildDataContainer.IsMasterBuilding(xy);

                            InfoBuidlingsDataContainer.RemoveXyBuild(buildType, key, xy);
                            CellBuildDataContainer.ResetBuild(xy);
                        }

                        CellEnvirDataContainer.ResetEnvironment(EnvironmentTypes.AdultForest, xy);

                        CellFireDataContainer.ResetFire(xy);


                        var aroundXYList = CellSpaceWorker.TryGetXyAround(xy);
                        foreach (var xy1 in aroundXYList)
                        {
                            if (CellViewContainer.IsActiveSelfParentCell(xy1))
                            {
                                if (CellEnvirDataContainer.HaveEnvironment(EnvironmentTypes.AdultForest, xy1))
                                {
                                    CellFireDataContainer.EnableFire(xy1);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}
