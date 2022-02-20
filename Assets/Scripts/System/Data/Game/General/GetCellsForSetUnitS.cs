namespace Game.Game
{
    sealed class GetCellsForSetUnitS : SystemAbstract, IEcsRunSystem
    {
        internal GetCellsForSetUnitS(in Entities ents) : base(ents)
        {
        }

        public void Run()
        {
            for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
            {
                for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
                {
                    E.UnitEs(idx_0).ForPlayer(player).CanSetUnitHere = false;
                }
            }

            //if (Es.BuildTC(idx_0).Is(BuildingTypes.Camp))
            //{
            //    if (!Es.MountainC(idx_0).HaveAny && !Es.UnitTC(idx_0).HaveUnit)
            //    {
            //        Es.UnitEs(idx_0).ForPlayer(player).CanSetUnitHere = true;
            //    }
            //}

            for (var player = PlayerTypes.None + 1; player < PlayerTypes.End; player++)
            {
                var idxs = E.PlayerE(player).LevelE(LevelTypes.First).BuildsInGame(BuildingTypes.City);

                if (idxs.HaveAny)
                {
                    foreach (var cellE in E.CellEs(idxs.IdxFirst).AroundCellIdxsC)
                    {
                        var idx_1 = cellE.Idx;

                        if (!E.MountainC(idx_1).HaveAny && !E.UnitTC(cellE.Idx).HaveUnit)
                        {
                            E.UnitEs(idx_1).ForPlayer(player).CanSetUnitHere = true;
                        }
                    }
                }
                else
                {
                    for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
                    {
                        var xy = E.CellEs(idx_0).CellE.XyC.Xy;
                        var x = xy[0];
                        var y = xy[1];

                        if (!E.UnitTC(idx_0).HaveUnit)
                        {
                            if (player == PlayerTypes.First)
                            {
                                if (y < 3 && x > 3 && x < 12)
                                {
                                    E.UnitEs(idx_0).ForPlayer(player).CanSetUnitHere = true;
                                }
                            }
                            else
                            {
                                if (y > 7 && x > 3 && x < 12)
                                {
                                    E.UnitEs(idx_0).ForPlayer(player).CanSetUnitHere = true;
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}