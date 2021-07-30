using Assets.Scripts;
using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Fire;
using Assets.Scripts.Workers.Game.Else.Info.Units;
using Assets.Scripts.Workers.Info;

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

                if (CellFireDataWorker.HaveFire(xy))
                {
                    CellFireDataWorker.AddTimeSteps(xy);

                    if (CellUnitsDataWorker.HaveAnyUnit(xy))
                    {
                        CellUnitsDataWorker.TakeAmountHealth(xy, 40);

                        if (!CellUnitsDataWorker.HaveAmountHealth(xy))
                        {
                            var conditionType = CellUnitsDataWorker.ConditionType(xy);
                            var unitType = CellUnitsDataWorker.UnitType(xy);
                            var key = CellUnitsDataWorker.IsMasterClient(xy);

                            InfoAmountUnitsWorker.RemoveAmountUnitsInGame(unitType, key, xy);
                            InfoUnitsConditionWorker.RemoveUnitInCondition(conditionType, unitType, key, xy);
                            CellUnitsDataWorker.ResetUnit(xy);
                        }
                    }



                    if (CellFireDataWorker.TimeSteps(xy) >= 3)
                    {

                        if (CellBuildingsDataWorker.HaveAnyBuilding(xy))
                        {
                            var buildType = CellBuildingsDataWorker.GetBuildingType(xy);
                            var key = CellBuildingsDataWorker.IsMasterBuilding(xy);

                            InfoBuidlingsWorker.RemoveXyBuild(buildType, key, xy);
                            CellBuildingsDataWorker.ResetBuild(xy);
                        }

                        CellEnvirDataWorker.ResetEnvironment(EnvironmentTypes.AdultForest, xy);

                        CellFireDataWorker.ResetFire(xy);
                        CellFireDataWorker.ResetTimeSteps(xy);


                        var aroundXYList = CellSpaceWorker.TryGetXYAround(xy);
                        foreach (var xy1 in aroundXYList)
                        {
                            if (CellEnvirDataWorker.HaveEnvironment(EnvironmentTypes.AdultForest, xy1))
                            {
                                CellFireDataWorker.EnableFire(xy1);
                            }
                        }
                    }
                }
            }
        }
    }
}
