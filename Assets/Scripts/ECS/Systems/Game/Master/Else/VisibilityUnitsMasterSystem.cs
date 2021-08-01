﻿using Assets.Scripts;
using Leopotam.Ecs;
using Photon.Pun;
using System.Collections.Generic;
using static Assets.Scripts.CellEnvirDataWorker;
using static Assets.Scripts.CellUnitsDataWorker;
using static Assets.Scripts.Workers.Cell.CellSpaceWorker;

internal sealed class VisibilityUnitsMasterSystem : IEcsRunSystem
{
    public void Run()
    {
        for (int x = 0; x < CellViewWorker.Xamount; x++)
            for (int y = 0; y < CellViewWorker.Yamount; y++)
            {
                var xy = new int[] { x, y };

                CellUnitsDataWorker.SetIsVisibleUnit(true, true, xy);
                CellUnitsDataWorker.SetIsVisibleUnit(false, true, xy);


                if (HaveAnyUnit(xy))
                {
                    if (HaveOwner(xy))
                    {
                        if (IsHim(PhotonNetwork.MasterClient, xy))
                        {
                            if (HaveEnvironment(EnvironmentTypes.AdultForest, xy))
                            {
                                CellUnitsDataWorker.SetIsVisibleUnit(false, false, xy);

                                List<int[]> list = TryGetXyAround(xy);
                                foreach (var xy1 in list)
                                {
                                    if (HaveAnyUnit(xy1))
                                    {
                                        if (HaveOwner(xy1))
                                        {
                                            if (!IsHim(PhotonNetwork.MasterClient, xy1))
                                            {
                                                CellUnitsDataWorker.SetIsVisibleUnit(false, true, xy);
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
                                CellUnitsDataWorker.SetIsVisibleUnit(true, false, xy);

                                List<int[]> list = TryGetXyAround(xy);
                                foreach (var xy1 in list)
                                {
                                    if (HaveAnyUnit(xy1))
                                    {
                                        if (HaveOwner(xy1))
                                        {
                                            if (IsHim(PhotonNetwork.MasterClient, xy1))
                                            {
                                                CellUnitsDataWorker.SetIsVisibleUnit(true, true, xy);
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
                            CellUnitsDataWorker.SetIsVisibleUnit(true, false, xy);

                            List<int[]> list = TryGetXyAround(xy);
                            foreach (var xy1 in list)
                            {
                                if (HaveAnyUnit(xy1))
                                {
                                    if (HaveOwner(xy1))
                                    {
                                        if (IsHim(PhotonNetwork.MasterClient, xy1))
                                        {
                                            CellUnitsDataWorker.SetIsVisibleUnit(true, true, xy);
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