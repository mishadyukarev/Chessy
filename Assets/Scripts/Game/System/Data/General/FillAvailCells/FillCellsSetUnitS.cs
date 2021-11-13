using Leopotam.Ecs;

namespace Chessy.Game
{
    public sealed class FillCellsSetUnitS : IEcsRunSystem
    {
        private EcsFilter<XyC> _xyCellFilter = default;
        private EcsFilter<UnitC> _cellUnitFilter = default;
        private EcsFilter<EnvC> _cellEnvFilt = default;
        private EcsFilter<BuildC, OwnerC> _cellBuldFilt = default;


        public void Run()
        {
            for (var player = Support.MinPlayerType; player < Support.MaxPlayerType; player++)
            {
                CellsForSetUnitC.ClearIdxCells(player);

                if (WhereBuildsC.IsSettedCity(player))
                {
                    var idx_city = WhereBuildsC.IdxCity(player);
                    ref var unit_city = ref _cellUnitFilter.Get1(idx_city);
                    
                    var listAround = CellSpaceSupport.GetXyAround(_xyCellFilter.Get1(idx_city).Xy);

                    if(!unit_city.HaveUnit) CellsForSetUnitC.AddIdxCell(player, idx_city);

                    foreach (var xy in listAround)
                    {
                        var curIdx = _xyCellFilter.GetIdxCell(xy);

                        ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdx);
                        ref var curEnvDatCom = ref _cellEnvFilt.Get1(curIdx);

                        if (!curEnvDatCom.Have(EnvTypes.Mountain) && !curUnitDatCom.HaveUnit)
                        {
                            CellsForSetUnitC.AddIdxCell(player, curIdx);
                        }
                    }
                }

                else
                {
                    foreach (byte curIdx in _xyCellFilter)
                    {
                        var xy = _xyCellFilter.Get1(curIdx).Xy;
                        var x = xy[0];
                        var y = xy[1];

                        ref var curUnitDatCom = ref _cellUnitFilter.Get1(curIdx);


                        if (!curUnitDatCom.HaveUnit)
                        {
                            if (player == PlayerTypes.First)
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
                ref var unit_0 = ref _cellUnitFilter.Get1(idx_0);
                ref var buld_0 = ref _cellBuldFilt.Get1(idx_0);
                ref var ownBuld_0 = ref _cellBuldFilt.Get2(idx_0);
                ref var env_0 = ref _cellEnvFilt.Get1(idx_0);

                if (buld_0.Is(BuildTypes.Camp))
                {
                    if (!env_0.Have(EnvTypes.Mountain) && !unit_0.HaveUnit)
                    {
                        CellsForSetUnitC.AddIdxCell(ownBuld_0.Owner, idx_0);
                    }
                }
            }
        }
    }
}
