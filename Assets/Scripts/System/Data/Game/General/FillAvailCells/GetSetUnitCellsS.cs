using static Game.Game.EntityCellPool;
using static Game.Game.EntityCellUnitPool;
using static Game.Game.EntityCellBuildPool;
using static Game.Game.EntityCellEnvPool;

namespace Game.Game
{
    struct GetSetUnitCellsS : IEcsRunSystem
    {
        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                SetUnitCellsC.Clear(player);

                if (WhereBuildsC.IsSetted(BuildTypes.City, player))
                {
                    var idx_city = WhereBuildsC.Idx(BuildTypes.City, player);
                    ref var unit_city = ref Build<BuildC>(idx_city);

                    var listAround = CellSpaceC.XyAround(Cell<XyC>(idx_city).Xy);

                    if (!unit_city.Have) SetUnitCellsC.AddIdxCell(player, idx_city);

                    foreach (var xy in listAround)
                    {
                        var idx_0 = IdxCell(xy);

                        ref var curUnitDatCom = ref Unit<UnitC>(idx_0);

                        if (!Environment<HaveEnvironmentC>(EnvTypes.Mountain, idx_0).Have && !curUnitDatCom.Have)
                        {
                            SetUnitCellsC.AddIdxCell(player, idx_0);
                        }
                    }
                }

                else
                {
                    foreach (byte idx_0 in Idxs)
                    {
                        var xy = Cell<XyC>(idx_0).Xy;
                        var x = xy[0];
                        var y = xy[1];

                        ref var curUnitDatCom = ref Unit<UnitC>(idx_0);


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

            foreach (byte idx_0 in Idxs)
            {
                ref var unit_0 = ref Unit<UnitC>(idx_0);
                ref var buld_0 = ref Build<BuildC>(idx_0);
                ref var ownBuld_0 = ref Build<OwnerC>(idx_0);

                if (buld_0.Is(BuildTypes.Camp))
                {
                    if (!Environment<HaveEnvironmentC>(EnvTypes.Mountain, idx_0).Have && !unit_0.Have)
                    {
                        SetUnitCellsC.AddIdxCell(ownBuld_0.Owner, idx_0);
                    }
                }
            }
        }
    }
}
