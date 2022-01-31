namespace Game.Game
{
    sealed class GetCellsForSetUnitS : SystemAbstract, IEcsRunSystem
    {
        public GetCellsForSetUnitS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (var player = PlayerTypes.First; player < PlayerTypes.End; player++)
            {
                foreach (var idx_0 in CellEs.Idxs)
                {
                    CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_0).Can = false;
                }

                if (Es.WhereBuildingEs.IsSetted(BuildingTypes.City, player, out var idx_1))
                {
                    var unit_1 = UnitEs.Main(idx_1).UnitTC;

                    if (unit_1.Have)
                    {
                        CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_1).Can = false;
                    }
                    else
                    {
                        CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_1).Can = true;
                    }

                    var list_2 = CellEs.GetXyAround(CellEs.CellE(idx_1).XyC.Xy);

                    foreach (var xy_2 in list_2)
                    {
                        var idx_2 = CellEs.GetIdxCell(xy_2);

                        var unit_2 = UnitEs.Main(idx_2).UnitTC;

                        if (!CellEs.EnvironmentEs.Mountain(idx_2).HaveEnvironment && !unit_2.Have)
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
                        var unit_0 = UnitEs.Main(idx_0).UnitTC;
                        var buld_0 = BuildEs.BuildingE(idx_0).BuildTC;
                        var ownBuld_0 = BuildEs.BuildingE(idx_0).Owner;


                        if (buld_0.Is(BuildingTypes.Camp))
                        {
                            if (!CellEs.EnvironmentEs.Mountain(idx_0).HaveEnvironment && !unit_0.Have)
                            {
                                CellsForSetUnitsEs.CanSet<CanSetUnitC>(player, idx_0).Can = true;
                            }
                        }
                        else
                        {
                            var xy = CellEs.CellE(idx_0).XyC.Xy;
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