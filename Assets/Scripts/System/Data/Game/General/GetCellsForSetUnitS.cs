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
                    Es.UnitEs(idx_0).ForPlayer(player).CanSetUnitHere = false;
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
                if (Es.PlayerE(player).LevelE(LevelTypes.First).BuildsInGame(BuildingTypes.City).HaveAny)
                {
                    for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
                    {
                        if (Es.UnitTC(idx_0).HaveUnit)
                        {
                            Es.UnitEs(idx_0).ForPlayer(player).CanSetUnitHere = false;
                        }
                        else
                        {
                            Es.UnitEs(idx_0).ForPlayer(player).CanSetUnitHere = true;
                        }

                        //foreach (var idx_2 in Es.CellEs(idx_0).Idxs)
                        //{
                        //    if (!Es.MountainC(idx_2).HaveAny && !Es.UnitTC(idx_2).HaveUnit)
                        //    {
                        //        Es.UnitEs(idx_2).ForPlayer(player).CanSetUnitHere = true;
                        //    }
                        //    else
                        //    {
                        //        Es.UnitEs(idx_2).ForPlayer(player).CanSetUnitHere = false;
                        //    }
                        //}
                    }
                }
                else
                {
                    for (byte idx_0 = 0; idx_0 < StartValues.ALL_CELLS_AMOUNT; idx_0++)
                    {
                        var xy = Es.CellEs(idx_0).CellE.XyC.Xy;
                        var x = xy[0];
                        var y = xy[1];

                        var canSet = false;

                        if (!Es.UnitTC(idx_0).HaveUnit)
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

                        Es.UnitEs(idx_0).ForPlayer(player).CanSetUnitHere = canSet;
                    }
                }
            }
        }
    }
}