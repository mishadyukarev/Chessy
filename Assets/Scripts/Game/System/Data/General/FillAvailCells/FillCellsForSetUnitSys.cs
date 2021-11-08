using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class FillCellsForSetUnitSys : IEcsRunSystem
    {
        private EcsFilter<XyCellComponent> _xyCellFilter = default;
        private EcsFilter<CellUnitDataC> _cellUnitFilter = default;
        private EcsFilter<CellEnvDataC> _cellEnvFilt = default;
        private EcsFilter<CellBuildDataC, OwnerCom> _cellBuldFilt = default;


        public void Run()
        {
            CellsForSetUnitC.ClearIdxCells(PlayerTypes.First);
            CellsForSetUnitC.ClearIdxCells(PlayerTypes.Second);

            for (var playerType = Support.MinPlayerType; playerType < Support.MaxPlayerType; playerType++)
            {
                if (WhereBuildsC.IsSettedCity(playerType))
                {
                    var idx_city = WhereBuildsC.IdxCity(playerType);
                    ref var unit_city = ref _cellUnitFilter.Get1(idx_city);
                    
                    var listAround = CellSpaceSupport.TryGetXyAround(_xyCellFilter.Get1(idx_city).XyCell);

                    if(!unit_city.HaveUnit) CellsForSetUnitC.AddIdxCell(playerType, idx_city);

                    foreach (var xy in listAround)
                    {
                        var curIdx = _xyCellFilter.GetIdxCell(xy);

                        ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdx);
                        ref var curEnvDatCom = ref _cellEnvFilt.Get1(curIdx);

                        if (!curEnvDatCom.Have(EnvTypes.Mountain) && !curUnitDatCom.HaveUnit)
                        {
                            CellsForSetUnitC.AddIdxCell(playerType, curIdx);
                        }
                    }
                }

                else
                {
                    foreach (byte curIdx in _xyCellFilter)
                    {
                        var xy = _xyCellFilter.Get1(curIdx).XyCell;
                        var x = xy[0];
                        var y = xy[1];

                        ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdx);


                        if (!curUnitDatCom.HaveUnit)
                        {
                            if (playerType == PlayerTypes.First)
                            {
                                if (y < 3 && x > 3 && x < 12)
                                {
                                    CellsForSetUnitC.AddIdxCell(PlayerTypes.First, curIdx);
                                }
                            }
                            else
                            {
                                if (y > 7 && x > 3 && x < 12)
                                {
                                    CellsForSetUnitC.AddIdxCell(PlayerTypes.Second, curIdx);
                                }
                            }
                        }
                    }
                }
            }

            foreach (byte idx_0 in _xyCellFilter)
            {
                ref var buld_0 = ref _cellBuldFilt.Get1(idx_0);
                ref var ownBuld_0 = ref _cellBuldFilt.Get2(idx_0);

                if (buld_0.Is(BuildTypes.Camp))
                {
                    var xyAround_1 = CellSpaceSupport.TryGetXyAround(_xyCellFilter.Get1(idx_0).XyCell);

                    foreach (var xy in xyAround_1)
                    {
                        var idx_1 = _xyCellFilter.GetIdxCell(xy);

                        ref var unit_1 = ref _cellUnitFilter.Get1(idx_1);
                        ref var env_1 = ref _cellEnvFilt.Get1(idx_1);

                        if (!env_1.Have(EnvTypes.Mountain) && !unit_1.HaveUnit)
                        {
                            CellsForSetUnitC.AddIdxCell(ownBuld_0.Owner, idx_1);
                        }
                    }
                }
            }
        }
    }
}
