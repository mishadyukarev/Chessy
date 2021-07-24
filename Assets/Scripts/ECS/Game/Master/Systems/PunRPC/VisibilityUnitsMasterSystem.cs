using Assets.Scripts;
using System.Collections.Generic;
using static Assets.Scripts.CellEnvironmentWorker;
using static Assets.Scripts.CellUnitWorker;
using static Assets.Scripts.Main;
using static Assets.Scripts.Workers.Cell.CellSpaceWorker;

internal sealed class VisibilityUnitsMasterSystem : SystemGeneralReduction
{

    public override void Run()
    {
        base.Run();


        for (int x = 0; x < _eGM.Xamount; x++)
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                var xy = new int[] { x, y };

                SetIsVisibleUnit(true, true, xy);
                SetIsVisibleUnit(false, true, xy);


                if (HaveAnyUnit(xy))
                {
                    if (HaveOwner(xy))
                    {
                        if (IsHim(Instance.MasterClient, xy))
                        {
                            if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                            {
                                SetIsVisibleUnit(false, false, xy);

                                List<int[]> list = TryGetXYAround(xy);
                                foreach (var xy1 in list)
                                {
                                    if (HaveAnyUnit(xy1))
                                    {
                                        if (HaveOwner(xy1))
                                        {
                                            if (!IsHim(Instance.MasterClient, xy1))
                                            {
                                                SetIsVisibleUnit(false, true, xy);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                            {
                                SetIsVisibleUnit(true, false, xy);

                                List<int[]> list = TryGetXYAround(xy);
                                foreach (var xy1 in list)
                                {
                                    if (HaveAnyUnit(xy1))
                                    {
                                        if (HaveOwner(xy1))
                                        {
                                            if (IsHim(Instance.MasterClient, xy1))
                                            {
                                                SetIsVisibleUnit(true, true, xy);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        var isActivatedVisionCellUnit = IsVisibleUnit(Instance.IsMasterClient, xy);

                        if (isActivatedVisionCellUnit)
                        {
                            SetEnabledUnit(true, xy);
                        }
                        else
                        {
                            SetEnabledUnit(false, xy);
                        }
                    }

                    else if (IsBot(xy))
                    {
                        if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        {
                            SetIsVisibleUnit(true, false, xy);

                            List<int[]> list = TryGetXYAround(xy);
                            foreach (var xy1 in list)
                            {
                                if (HaveAnyUnit(xy1))
                                {
                                    if (HaveOwner(xy1))
                                    {
                                        if (IsHim(Instance.MasterClient, xy1))
                                        {
                                            SetIsVisibleUnit(true, true, xy);
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        var isActivatedVisionCellUnit = IsVisibleUnit(Instance.IsMasterClient, xy);

                        if (isActivatedVisionCellUnit)
                        {
                            SetEnabledUnit(true, xy);
                        }
                        else
                        {
                            SetEnabledUnit(false, xy);
                        }
                    }
                }
            }
    }
}