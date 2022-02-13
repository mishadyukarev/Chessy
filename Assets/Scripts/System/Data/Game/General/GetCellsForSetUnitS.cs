namespace Game.Game
{
    sealed class GetCellsForSetUnitS : SystemAbstract, IEcsRunSystem
    {
        internal GetCellsForSetUnitS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                foreach (var idx_0 in CellWorker.Idxs)
                {
                    CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_0).Can = false;
                }

                if (Es.WhereWorker.TryGetBuilding(BuildingTypes.City, player, out var idx_1))
                {
                    if (UnitEs(idx_1).UnitE.HaveUnit)
                    {
                        CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_1).Can = false;
                    }
                    else
                    {
                        CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_1).Can = true;
                    }

                    var list_2 = CellWorker.GetXyAround(CellEs(idx_1).CellE.XyC.Xy);

                    foreach (var xy_2 in list_2)
                    {
                        var idx_2 = CellWorker.GetIdxCell(xy_2);

                        if (!EnvironmentEs(idx_2).Mountain.HaveEnvironment && !UnitEs(idx_2).UnitE.HaveUnit)
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
                    foreach (var idx_0 in CellWorker.Idxs)
                    {
                        if (Es.BuildingE(idx_0).Is(BuildingTypes.Camp))
                        {
                            if (!EnvironmentEs(idx_0).Mountain.HaveEnvironment && !UnitEs(idx_0).UnitE.HaveUnit)
                            {
                                CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_0).Can = true;
                            }
                        }
                        else
                        {
                            var xy = CellEs(idx_0).CellE.XyC.Xy;
                            var x = xy[0];
                            var y = xy[1];

                            var canSet = false;

                            if (!UnitEs(idx_0).UnitE.HaveUnit)
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