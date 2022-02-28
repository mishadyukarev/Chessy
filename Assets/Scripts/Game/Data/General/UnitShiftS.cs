namespace Chessy.Game
{
    sealed class UnitShiftS : SystemAbstract, IEcsRunSystem
    {
        internal UnitShiftS(in EntitiesModel ents) : base(ents)
        {

        }

        public void Run()
        {
            for (byte idx_from = 0; idx_from < Start_Values.ALL_CELLS_AMOUNT; idx_from++)
            {
                var idx_to = E.UnitMainE(idx_from).ShiftTo.Idx;

                if (idx_to ==  0) continue;


               


                E.UnitEs(idx_to).Set(E.UnitEs(idx_from));
                E.UnitConditionTC(idx_to).Condition = ConditionUnitTypes.None;

                E.UnitTC(idx_from).Unit = UnitTypes.None;


                var direct = E.CellEs(idx_from).Direct(idx_to);

                if (!E.UnitTC(idx_to).Is(UnitTypes.Undead))
                {
                    if (E.AdultForestC(idx_from).HaveAnyResources)
                    {
                        E.CellEs(idx_from).TrailHealthC(direct).Health = CellTrail_Values.HEALTH_TRAIL;
                    }
                    if (E.AdultForestC(idx_to).HaveAnyResources)
                    {
                        E.CellEs(idx_to).TrailHealthC(direct.Invert()).Health = CellTrail_Values.HEALTH_TRAIL;
                    }

                    if (E.RiverEs(idx_to).RiverTC.HaveRiverNear)
                    {
                        E.UnitWaterC(idx_to).Water = E.UnitInfo(E.UnitPlayerTC(idx_to), E.UnitLevelTC(idx_to), E.UnitTC(idx_to)).MaxWater;
                    }
                }

                if (E.UnitTC(idx_to).Is(UnitTypes.Hell))
                {
                    if (E.AdultForestC(idx_to).HaveAnyResources)
                    {
                        E.EffectEs(idx_to).HaveFire = true;
                    }
                }

                if (E.BuildingTC(idx_to).HaveBuilding && !E.BuildingTC(idx_to).Is(BuildingTypes.City))
                {
                    if (!E.BuildingPlayerTC(idx_to).Is(E.UnitPlayerTC(idx_to).Player))
                    {
                        E.BuildingTC(idx_to).Building = BuildingTypes.None;
                    }
                }

                E.UnitMainE(idx_from).ShiftTo.Idx = 0;
                E.UnitMainE(idx_to).ShiftTo.Idx = 0;
            }
        }
    }
}