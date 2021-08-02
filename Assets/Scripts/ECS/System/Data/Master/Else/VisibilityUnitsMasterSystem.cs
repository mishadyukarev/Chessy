using Assets.Scripts;
using Assets.Scripts.Abstractions.ValuesConsts;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using static Assets.Scripts.CellEnvirDataContainer;
using static Assets.Scripts.CellUnitsDataContainer;
using static Assets.Scripts.Workers.Cell.CellSpaceWorker;

internal sealed class VisibilityUnitsMasterSystem : IEcsRunSystem
{
    public void Run()
    {
        for (int x = 0; x < CellValues.CELL_COUNT_X; x++)
            for (int y = 0; y < CellValues.CELL_COUNT_Y; y++)
            {
                var xy = new int[] { x, y };

                CellUnitsDataContainer.SetIsVisibleUnit(true, true, xy);
                CellUnitsDataContainer.SetIsVisibleUnit(false, true, xy);


                if (HaveAnyUnit(xy))
                {
                    if (HaveOwner(xy))
                    {
                        if (IsHim(PhotonNetwork.MasterClient, xy))
                        {
                            if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                            {
                                CellUnitsDataContainer.SetIsVisibleUnit(false, false, xy);

                                List<int[]> list = TryGetXyAround(xy);
                                foreach (var xy1 in list)
                                {
                                    if (HaveAnyUnit(xy1))
                                    {
                                        if (HaveOwner(xy1))
                                        {
                                            if (!IsHim(PhotonNetwork.MasterClient, xy1))
                                            {
                                                CellUnitsDataContainer.SetIsVisibleUnit(false, true, xy);
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
                                CellUnitsDataContainer.SetIsVisibleUnit(true, false, xy);

                                List<int[]> list = TryGetXyAround(xy);
                                foreach (var xy1 in list)
                                {
                                    if (HaveAnyUnit(xy1))
                                    {
                                        if (HaveOwner(xy1))
                                        {
                                            if (IsHim(PhotonNetwork.MasterClient, xy1))
                                            {
                                                CellUnitsDataContainer.SetIsVisibleUnit(true, true, xy);
                                                break;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    else if (IsBot(xy))
                    {
                        if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                        {
                            CellUnitsDataContainer.SetIsVisibleUnit(true, false, xy);

                            List<int[]> list = TryGetXyAround(xy);
                            foreach (var xy1 in list)
                            {
                                if (HaveAnyUnit(xy1))
                                {
                                    if (HaveOwner(xy1))
                                    {
                                        if (IsHim(PhotonNetwork.MasterClient, xy1))
                                        {
                                            CellUnitsDataContainer.SetIsVisibleUnit(true, true, xy);
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