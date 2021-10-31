using Leopotam.Ecs;
using System;

namespace Scripts.Game
{
    internal sealed class FillCellsForSetUnitSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataCom> _cellUnitFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilt = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuldFilt = default;

        private EcsFilter<CellsForSetUnitComp> _cellsForSetUnitFilter = default;
        private EcsFilter<WhereBuildsC> _buildsInGameFilt = default;


        public void Run()
        {
            ref var forSetUnitCom = ref _cellsForSetUnitFilter.Get1(0);
            ref var buildsInGameCom = ref _buildsInGameFilt.Get1(0);

            forSetUnitCom.ClearIdxCells(PlayerTypes.First);
            forSetUnitCom.ClearIdxCells(PlayerTypes.Second);

            for (var playerType = Support.MinPlayerType; playerType < Support.MaxPlayerType; playerType++)
            {
                if (WhereBuildsC.IsSettedCity(playerType))
                {
                    var listAround = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(WhereBuildsC.IdxCity(playerType)));

                    foreach (var xy in listAround)
                    {
                        var curIdx = _xyCellFilter.GetIdxCell(xy);

                        ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdx);
                        ref var curEnvDatCom = ref _cellEnvFilt.Get1(curIdx);

                        if (!curEnvDatCom.Have(EnvirTypes.Mountain) && !curUnitDatCom.HaveUnit)
                        {
                            forSetUnitCom.AddIdxCell(playerType, curIdx);
                        }
                    }
                }

                else
                {
                    foreach (byte curIdx in _xyCellFilter)
                    {
                        var xy = _xyCellFilter.GetXyCell(curIdx);
                        var x = xy[0];
                        var y = xy[1];

                        ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdx);


                        if (!curUnitDatCom.HaveUnit)
                        {
                            if (playerType == PlayerTypes.First)
                            {
                                if (y < 3 && x > 3 && x < 12)
                                {
                                    forSetUnitCom.AddIdxCell(PlayerTypes.First, curIdx);
                                }
                            }
                            else
                            {
                                if (y > 7 && x > 3 && x < 12)
                                {
                                    forSetUnitCom.AddIdxCell(PlayerTypes.Second, curIdx);
                                }
                            }
                        }
                    }
                }
            }

            foreach (byte curIdx in _xyCellFilter)
            {
                ref var curBuldCom = ref _cellBuldFilt.Get1(curIdx);
                ref var curOwnBuldCom = ref _cellBuldFilt.Get2(curIdx);

                if (curBuldCom.Is(BuildTypes.Camp))
                {
                    var aroundXys = CellSpaceSupport.TryGetXyAround(_xyCellFilter.GetXyCell(curIdx));

                    foreach (var xy in aroundXys)
                    {
                        var curIdx_2 = _xyCellFilter.GetIdxCell(xy);

                        ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdx_2);
                        ref var curEnvDatCom = ref _cellEnvFilt.Get1(curIdx_2);

                        if (!curEnvDatCom.Have(EnvirTypes.Mountain) && !curUnitDatCom.HaveUnit)
                        {
                            forSetUnitCom.AddIdxCell(curOwnBuldCom.Owner, curIdx_2);
                        }
                    }
                }
            }
        }
    }
}
