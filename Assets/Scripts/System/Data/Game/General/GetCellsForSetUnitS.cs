namespace Game.Game
{
    public struct GetCellsForSetUnitS : IEcsRunSystem
    {
        public void Run()
        {
            var canSet = false;

            foreach (var idx_0 in CellEs.Idxs)
            {
                for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
                {
                    canSet = false;

                    ref var unit_0 = ref CellUnitEs.Unit<UnitTC>(idx_0);
                    ref var buld_0 = ref CellBuildE.Build<BuildingC>(idx_0);
                    ref var ownBuld_0 = ref CellBuildE.Build<PlayerTC>(idx_0);

                    if (WhereBuildsE.IsSetted(BuildTypes.City, player, out var idx_city))
                    {
                        ref var unit_city = ref CellBuildE.Build<BuildingC>(idx_city);

                        var list_1 = CellSpaceC.XyAround(CellEs.Cell<XyC>(idx_city).Xy);

                        if (!unit_city.Have) canSet = true;

                        foreach (var xy_1 in list_1)
                        {
                            var idx_1 = CellEs.IdxCell(xy_1);

                            ref var unit_1 = ref CellUnitEs.Unit<UnitTC>(idx_1);

                            if (!CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvTypes.Mountain, idx_1).Have && !unit_1.Have)
                            {
                                canSet = true;
                            }
                        }
                    }

                    else
                    {
                        var xy = CellEs.Cell<XyC>(idx_0).Xy;
                        var x = xy[0];
                        var y = xy[1];

                        ref var unit_cur = ref CellUnitEs.Unit<UnitTC>(idx_0);

                        if (!unit_cur.Have)
                        {
                            if (player == PlayerTypes.First)
                            {
                                if (y < 3 && x > 3 && x < 12)
                                {
                                    canSet = true;
                                }
                            }
                            else
                            {
                                if (y > 7 && x > 3 && x < 12)
                                {
                                    canSet = true;
                                }
                            }
                        }
                    }

                    if (buld_0.Is(BuildTypes.Camp))
                    {
                        if (!CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvTypes.Mountain, idx_0).Have && !unit_0.Have)
                        {
                            canSet = true;
                        }
                    }

                    CellsForSetUnitEs.CanSet<CanSetUnitC>(player, idx_0).Can = canSet;
                }
            }
        }
    }
}