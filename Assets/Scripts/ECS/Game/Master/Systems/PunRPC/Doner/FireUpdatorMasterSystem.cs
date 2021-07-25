using Assets.Scripts;
using Assets.Scripts.Workers;
using Assets.Scripts.Workers.Cell;
using Assets.Scripts.Workers.Game.Else.Fire;

internal sealed class FireUpdatorMasterSystem : SystemMasterReduction
{

    public override void Run()
    {
        base.Run();

        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                var xy = new int[] { x, y };

                if (CellFireDataWorker.HaveFire(xy))
                {
                    CellFireDataWorker.AddTimeSteps(xy);

                    CellUnitsDataWorker.TakeAmountHealth(xy, 40);

                    if (!CellUnitsDataWorker.HaveAmountHealth(xy))
                    {
                        CellUnitsDataWorker.ResetUnit(xy);
                    }


                    if (CellFireDataWorker.TimeSteps(xy) >= 3)
                    {
                        CellBuildingsDataWorker.ResetBuilding(xy);
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
