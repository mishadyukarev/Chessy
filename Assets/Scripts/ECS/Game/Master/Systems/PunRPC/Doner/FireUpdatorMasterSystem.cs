using Assets.Scripts;
using Assets.Scripts.Workers;
using static Assets.Scripts.Workers.Cell.CellEffectsWorker;
using static Assets.Scripts.CellEnvironmentWorker;
using static Assets.Scripts.CellUnitWorker;
using Assets.Scripts.Workers.Cell;

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

                if (HaveEffect(EffectTypes.Fire, xy))
                {
                    AddTimeStepsEffect(EffectTypes.Fire, xy);

                    TakeAmountHealth(xy, 40);
                    if (!HaveAmountHealth(xy))
                    {
                        if (HaveOwner(xy))
                        {
                            ResetPlayerUnit(xy);
                        }
                        else
                        {
                            ResetBotUnit(x, y);
                        }

                    }


                    if (TimeStepsEffect(EffectTypes.Fire, xy) >= 3)
                    {
                        if (CellBuildingWorker.HaveBuilding(xy))
                        {
                            CellBuildingWorker.ResetPlayerBuilding(x, y);
                        }


                        ResetEnvironment(EnvironmentTypes.AdultForest, xy);

                        ResetEffect(EffectTypes.Fire, xy);


                        var aroundXYList = CellSpaceWorker.TryGetXYAround(xy);
                        foreach (var xy1 in aroundXYList)
                        {
                            if (HaveEnvironment(EnvironmentTypes.AdultForest, xy1))
                            {
                                SetEffect(EffectTypes.Fire, xy1);
                            }
                        }

                        SetTimeStepsEffect(EffectTypes.Fire, 0, xy);
                    }

                }
            }
        }
    }
}
