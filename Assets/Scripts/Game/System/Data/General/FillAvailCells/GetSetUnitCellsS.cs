using Leopotam.Ecs;

namespace Game.Game
{
    public sealed class GetSetUnitCellsS : IEcsRunSystem
    {
        private EcsFilter<UnitC> _unitF = default;
        private EcsFilter<EnvC> _envF = default;

        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                SetUnitCellsC.Clear(player);

                if (WhereBuildsC.IsSetted(BuildTypes.City, player))
                {
                    var idx_city = WhereBuildsC.Idx(BuildTypes.City, player);
                    ref var unit_city = ref _unitF.Get1(idx_city);
                    
                    var listAround = CellSpace.GetXyAround(EntityDataPool.GetCellC<XyC>(idx_city).Xy);

                    if(!unit_city.HaveUnit) SetUnitCellsC.AddIdxCell(player, idx_city);

                    foreach (var xy in listAround)
                    {
                        var curIdx = EntityDataPool.GetIdxCell(xy);

                        ref var curUnitDatCom = ref _unitF.Get1(curIdx);
                        ref var curEnvDatCom = ref _envF.Get1(curIdx);

                        if (!curEnvDatCom.Have(EnvTypes.Mountain) && !curUnitDatCom.HaveUnit)
                        {
                            SetUnitCellsC.AddIdxCell(player, curIdx);
                        }
                    }
                }

                else
                {
                    for (byte idx_0 = 0; idx_0 < EntityDataPool.AmountAllCells; idx_0++)
                    {
                        var xy = EntityDataPool.GetCellC<XyC>(idx_0).Xy;
                        var x = xy[0];
                        var y = xy[1];

                        ref var curUnitDatCom = ref _unitF.Get1(idx_0);


                        if (!curUnitDatCom.HaveUnit)
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

            for (byte idx_0 = 0; idx_0 < EntityDataPool.AmountAllCells; idx_0++)
            {
                ref var unit_0 = ref _unitF.Get1(idx_0);
                ref var buld_0 = ref EntityDataPool.GetBuildCellC<BuildC>(idx_0);
                ref var ownBuld_0 = ref EntityDataPool.GetBuildCellC<OwnerC>(idx_0);
                ref var env_0 = ref _envF.Get1(idx_0);

                if (buld_0.Is(BuildTypes.Camp))
                {
                    if (!env_0.Have(EnvTypes.Mountain) && !unit_0.HaveUnit)
                    {
                        SetUnitCellsC.AddIdxCell(ownBuld_0.Owner, idx_0);
                    }
                }
            }
        }
    }
}
