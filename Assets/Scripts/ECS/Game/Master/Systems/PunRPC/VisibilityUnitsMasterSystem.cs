using Assets.Scripts;
using System.Collections.Generic;
using static Assets.Scripts.Main;
using static Assets.Scripts.CellEnvironmentWorker;
using static Assets.Scripts.CellUnitWorker;
using static Assets.Scripts.Static.Cell.CellSpaceWorker;
using Assets.Scripts.Static.Cell;

internal sealed class VisibilityUnitsMasterSystem : SystemGeneralReduction
{

    public override void Run()
    {
        base.Run();


        for (int x = 0; x < _eGM.Xamount; x++)
        {
            for (int y = 0; y < _eGM.Yamount; y++)
            {
                var xy = new int[] { x, y };

                SetIsActivated(true, true, xy);
                SetIsActivated(false, true, xy);


                if (HaveAnyUnit(xy))
                {
                    if (HaveOwner(xy))
                    {
                        if (IsHim(Instance.MasterClient, xy))
                        {
                            if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                            {
                                SetIsActivated(false, false, xy);

                                List<int[]> list = CellSpaceWorker.TryGetXYAround(xy);
                                foreach (var xy1 in list)
                                {
                                    if (_eGM.CellUnitEnt_UnitTypeCom(xy1).HaveAnyUnit)
                                    {
                                        if (_eGM.CellUnitEnt_CellOwnerCom(xy1).HaveOwner)
                                        {
                                            if (!_eGM.CellUnitEnt_CellOwnerCom(xy1).IsHim(Instance.MasterClient))
                                            {
                                                SetIsActivated(false, true, xy);
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
                                SetIsActivated(true, false, xy);

                                List<int[]> list = TryGetXYAround(xy);
                                foreach (var xy1 in list)
                                {
                                    if (_eGM.CellUnitEnt_UnitTypeCom(xy1).HaveAnyUnit)
                                    {
                                        if (_eGM.CellUnitEnt_CellOwnerCom(xy1).HaveOwner)
                                        {
                                            if (_eGM.CellUnitEnt_CellOwnerCom(xy1).IsHim(Instance.MasterClient))
                                            {
                                                SetIsActivated(true, true, xy);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        var isActivatedVisionCellUnit = IsActivated(Instance.IsMasterClient, xy);

                        if (isActivatedVisionCellUnit)
                        {
                            SetEnabled(true, xy);
                        }
                        else
                        {
                            SetEnabled(false, xy);
                        }
                    }

                    else if (HaveBot(xy))
                    {
                        if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        {
                            SetIsActivated(true, false, xy);

                            List<int[]> list = TryGetXYAround(xy);
                            foreach (var xy1 in list)
                            {
                                if (_eGM.CellUnitEnt_UnitTypeCom(xy1).HaveAnyUnit)
                                {
                                    if (_eGM.CellUnitEnt_CellOwnerCom(xy1).HaveOwner)
                                    {
                                        if (_eGM.CellUnitEnt_CellOwnerCom(xy1).IsHim(Instance.MasterClient))
                                        {
                                            SetIsActivated(true, true, xy);
                                            break;
                                        }
                                    }
                                }
                            }
                        }

                        var isActivatedVisionCellUnit = IsActivated(Instance.IsMasterClient, xy);

                        if (isActivatedVisionCellUnit)
                        {
                            SetEnabled(true, xy);
                        }
                        else
                        {
                            SetEnabled(false, xy);
                        }
                    }
                }
            }
        }
    }
}