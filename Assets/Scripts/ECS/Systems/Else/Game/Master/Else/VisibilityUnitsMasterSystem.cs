using Assets.Scripts.ECS.Game.General.Components;
using Leopotam.Ecs;

internal sealed class VisibilityUnitsMasterSystem : IEcsRunSystem
{
    private EcsFilter<CellUnitComponent, OwnerComponent, OwnerBotComponent> _cellUnitFilter;

    public void Run()
    {
        foreach (var idxCurCell in _cellUnitFilter)
        {
            ref var cellUnitDataCom = ref _cellUnitFilter.Get1(idxCurCell);

            cellUnitDataCom.SetIsVisibleUnit(true, true);
            cellUnitDataCom.SetIsVisibleUnit(false, true);


            //if (CellUnitsDataSystem.HaveAnyUnit(xy))
            //{
            //    if (CellUnitsDataSystem.HaveOwner(xy))
            //    {
            //        if (CellUnitsDataSystem.IsHim(PhotonNetwork.MasterClient, xy))
            //        {
            //            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
            //            {
            //                CellUnitsDataSystem.SetIsVisibleUnit(false, false, xy);

            //                List<int[]> list = TryGetXyAround(xy);
            //                foreach (var xy1 in list)
            //                {
            //                    if (CellUnitsDataSystem.HaveAnyUnit(xy1))
            //                    {
            //                        if (CellUnitsDataSystem.HaveOwner(xy1))
            //                        {
            //                            if (!CellUnitsDataSystem.IsHim(PhotonNetwork.MasterClient, xy1))
            //                            {
            //                                CellUnitsDataSystem.SetIsVisibleUnit(false, true, xy);
            //                                break;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
            //            {
            //                CellUnitsDataSystem.SetIsVisibleUnit(true, false, xy);

            //                List<int[]> list = TryGetXyAround(xy);
            //                foreach (var xy1 in list)
            //                {
            //                    if (CellUnitsDataSystem.HaveAnyUnit(xy1))
            //                    {
            //                        if (CellUnitsDataSystem.HaveOwner(xy1))
            //                        {
            //                            if (CellUnitsDataSystem.IsHim(PhotonNetwork.MasterClient, xy1))
            //                            {
            //                                CellUnitsDataSystem.SetIsVisibleUnit(true, true, xy);
            //                                break;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    else if (CellUnitsDataSystem.IsBot(xy))
            //    {
            //        if (CellEnvrDataSystem.HaveEnvironment(EnvironmentTypes.AdultForest, xy))
            //        {
            //            CellUnitsDataSystem.SetIsVisibleUnit(true, false, xy);

            //            List<int[]> list = TryGetXyAround(xy);
            //            foreach (var xy1 in list)
            //            {
            //                if (CellUnitsDataSystem.HaveAnyUnit(xy1))
            //                {
            //                    if (CellUnitsDataSystem.HaveOwner(xy1))
            //                    {
            //                        if (CellUnitsDataSystem.IsHim(PhotonNetwork.MasterClient, xy1))
            //                        {
            //                            CellUnitsDataSystem.SetIsVisibleUnit(true, true, xy);
            //                            break;
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}
        }
    }
}