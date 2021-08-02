using Assets.Scripts.Abstractions.ValuesConsts;
using Assets.Scripts.ECS.System.Data.Game.General.Cell;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using static Assets.Scripts.Workers.Cell.CellSpaceWorker;

internal sealed class VisibilityUnitsMasterSystem : IEcsRunSystem
{
    public void Run()
    {
        for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

                CellUnitsDataSystem.SetIsVisibleUnit(true, true, xy);
                CellUnitsDataSystem.SetIsVisibleUnit(false, true, xy);


                if (CellUnitsDataSystem.HaveAnyUnit(xy))
                {
                    if (CellUnitsDataSystem.HaveOwner(xy))
                    {
                        if (CellUnitsDataSystem.IsHim(PhotonNetwork.MasterClient, xy))
                        {
                            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                            {
                                CellUnitsDataSystem.SetIsVisibleUnit(false, false, xy);

                                List<int[]> list = TryGetXyAround(xy);
                                foreach (var xy1 in list)
                                {
                                    if (CellUnitsDataSystem.HaveAnyUnit(xy1))
                                    {
                                        if (CellUnitsDataSystem.HaveOwner(xy1))
                                        {
                                            if (!CellUnitsDataSystem.IsHim(PhotonNetwork.MasterClient, xy1))
                                            {
                                                CellUnitsDataSystem.SetIsVisibleUnit(false, true, xy);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                            {
                                CellUnitsDataSystem.SetIsVisibleUnit(true, false, xy);

                                List<int[]> list = TryGetXyAround(xy);
                                foreach (var xy1 in list)
                                {
                                    if (CellUnitsDataSystem.HaveAnyUnit(xy1))
                                    {
                                        if (CellUnitsDataSystem.HaveOwner(xy1))
                                        {
                                            if (CellUnitsDataSystem.IsHim(PhotonNetwork.MasterClient, xy1))
                                            {
                                                CellUnitsDataSystem.SetIsVisibleUnit(true, true, xy);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if (CellUnitsDataSystem.IsBot(xy))
                    {
                        if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        {
                            CellUnitsDataSystem.SetIsVisibleUnit(true, false, xy);

                            List<int[]> list = TryGetXyAround(xy);
                            foreach (var xy1 in list)
                            {
                                if (CellUnitsDataSystem.HaveAnyUnit(xy1))
                                {
                                    if (CellUnitsDataSystem.HaveOwner(xy1))
                                    {
                                        if (CellUnitsDataSystem.IsHim(PhotonNetwork.MasterClient, xy1))
                                        {
                                            CellUnitsDataSystem.SetIsVisibleUnit(true, true, xy);
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
    }
}