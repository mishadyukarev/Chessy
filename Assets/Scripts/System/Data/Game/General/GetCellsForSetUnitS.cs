namespace Game.Game
{
    public struct GetCellsForSetUnitS : IEcsRunSystem
    {
        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                foreach (var idx_0 in CellEs.Idxs)
                {
                    CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_0).Can = false;
                }

                if (WhereBuildsE.IsSetted(BuildingTypes.City, player, out var idx_1))
                {
                    ref var unit_1 = ref CellUnitEs.Unit<UnitTC>(idx_1);

                    if (unit_1.Have)
                    {
                        CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_1).Can = false;
                    }
                    else
                    {
                        CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_1).Can = true;
                    }

                    var list_2 = CellSpaceC.GetXyAround(CellEs.Cell<XyC>(idx_1).Xy);

                    foreach (var xy_2 in list_2)
                    {
                        var idx_2 = CellEs.IdxCell(xy_2);

                        ref var unit_2 = ref CellUnitEs.Unit<UnitTC>(idx_2);

                        if (!CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvTypes.Mountain, idx_2).Have && !unit_2.Have)
                        {
                            CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_2).Can = true;
                        }
                        else
                        {
                            CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_2).Can = false;
                        }
                    }
                }

                else
                {
                    foreach (var idx_0 in CellEs.Idxs)
                    {
                        ref var unit_0 = ref CellUnitEs.Unit<UnitTC>(idx_0);
                        ref var buld_0 = ref CellBuildE.Build<BuildingTC>(idx_0);
                        ref var ownBuld_0 = ref CellBuildE.Build<PlayerTC>(idx_0);


                        if (buld_0.Is(BuildingTypes.Camp))
                        {
                            if (!CellEnvironmentEs.Environment<HaveEnvironmentC>(EnvTypes.Mountain, idx_0).Have && !unit_0.Have)
                            {
                                CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_0).Can = true;
                            }
                        }
                        else
                        {
                            var xy = CellEs.Cell<XyC>(idx_0).Xy;
                            var x = xy[0];
                            var y = xy[1];

                            var canSet = false;

                            if (!unit_0.Have)
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

                            CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_0).Can = canSet;
                        }
                    }
                }
            }
        }
    }
}