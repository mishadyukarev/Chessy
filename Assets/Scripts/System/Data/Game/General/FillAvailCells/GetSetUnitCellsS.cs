using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class GetSetUnitCellsS : IEcsRunSystem
    {
        private EcsFilter<UnitC> _unitF = default;
        private EcsFilter<EnvironmentC> _envF = default;

        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                SetUnitCellsC.Clear(player);

                if (WhereBuildsC.IsSetted(BuildTypes.City, player))
                {
                    var idx_city = WhereBuildsC.Idx(BuildTypes.City, player);
                    ref var unit_city = ref _unitF.Get1(idx_city);
                    
                    var listAround = CellSpaceC.XyAround(EntityCellPool.Cell<XyC>(idx_city).Xy);

                    if(!unit_city.Have) SetUnitCellsC.AddIdxCell(player, idx_city);

                    foreach (var xy in listAround)
                    {
                        var curIdx = EntityCellPool.IdxCell(xy);

                        ref var curUnitDatCom = ref _unitF.Get1(curIdx);
                        ref var curEnvDatCom = ref _envF.Get1(curIdx);

                        if (!curEnvDatCom.Have(EnvTypes.Mountain) && !curUnitDatCom.Have)
                        {
                            SetUnitCellsC.AddIdxCell(player, curIdx);
                        }
                    }
                }

                else
                {
                    foreach (byte idx_0 in EntityCellPool.Idxs)
                    {
                        var xy = EntityCellPool.Cell<XyC>(idx_0).Xy;
                        var x = xy[0];
                        var y = xy[1];

                        ref var curUnitDatCom = ref _unitF.Get1(idx_0);


                        if (!curUnitDatCom.Have)
                        {
                            if (player == PlayerTypes.First)
                            {
                                if (y < 3 && x > 3 && x < 12)
                                {
                                    SetUnitCellsC.AddIdxCell(PlayerTypes.First, idx_0);
                                }
                            }
                            else
                            {
                                if (y > 7 && x > 3 && x < 12)
                                {
                                    SetUnitCellsC.AddIdxCell(PlayerTypes.Second, idx_0);
                                }
                            }
                        }
                    }
                }
            }

            foreach (byte idx_0 in EntityCellPool.Idxs)
            {
                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var buld_0 = ref EntityCellPool.Build<BuildC>(idx_0);
                ref var ownBuld_0 = ref EntityCellPool.Build<OwnerC>(idx_0);
                ref var env_0 = ref _envF.Get1(idx_0);

                if (buld_0.Is(BuildTypes.Camp))
                {
                    if (!env_0.Have(EnvTypes.Mountain) && !unit_0.Have)
                    {
                        SetUnitCellsC.AddIdxCell(ownBuld_0.Owner, idx_0);
                    }
                }
            }
        }
    }
}
